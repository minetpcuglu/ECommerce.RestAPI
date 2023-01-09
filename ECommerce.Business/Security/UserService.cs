
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Business.Aspects.AutoFac;
using ECommerce.Core.Utilities.IoC;
using ECommerce.Core.Utilities.Results;
using ECommerce.Core.Utilities.Security.Encryption;
using ECommerce.Core.Utilities.Security.Hashing;
using ECommerce.Core.Utilities.Security.Jwt;
using ECommerce.Core.Utilities.Security.Request;
using ECommerce.Data.EF.Security;
using ECommerce.Localization;
using ECommerce.Shared.CriteriaObjects;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.DataTransferObjects;
using ECommerce.Shared.DataTransferObjects.Security;
using ECommerce.Shared.Request.Security;

namespace ECommerce.Business.Security
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ProtectHelper _protectHelper;
       private readonly IUserRoleService _userRoleService;
       private readonly ITokenHelper _tokenHelper;

        public UserService(IUserRepository userRepository, IUserRoleService userRoleService, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _protectHelper = new ProtectHelper();
            _userRoleService = userRoleService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<UserTokenDTO>> GetUserLoginAsync(LoginCO co)
        {
            if (string.IsNullOrEmpty(co.UserName) || string.IsNullOrEmpty(co.Password))
            {
                return new ErrorDataResult<UserTokenDTO>(
                    "Kullanıcı Adı veya Şifreniz Hatalı. Lütfen Tekrar Deneyiniz!");
            }

            co.UserName = co.UserName.ToLower(new CultureInfo("en-US", false)).Trim();

            co.UserName = _protectHelper.XssProtect(co.UserName);

            var kullanici = await _userRepository.GetUserLoginAsync(co.UserName);
            if (kullanici == null)
            {
                return new ErrorDataResult<UserTokenDTO>(
                    "Kullanıcı Adı veya Şifreniz Hatalı. Lütfen Tekrar Deneyiniz!");
            }


            if (!HashingHelper.VerifyPasswordHash(co.Password, kullanici.Password, kullanici.TuzlamaDegeri))
            {
                return new ErrorDataResult<UserTokenDTO>(
                    "Kullanıcı Adı veya Şifreniz Hatalı. Lütfen Tekrar Deneyiniz!");
            }

            if (!kullanici.IsActive)
            {
                return new ErrorDataResult<UserTokenDTO>(
                    "Durumunuz aktif olmadığıdan giriş yapılamaz!");
            }


            var kullaniciYetkiler = await _userRoleService.UserAuthorizationsAsync(kullanici.Id);

            var token = _tokenHelper.TokenOlustur(kullanici, kullaniciYetkiler.Data);
            var kullaniciToken = new UserTokenDTO
            {
                UserDetail = kullanici,
                TokenDetail = token,

                // SubeList = subeler.Select(x=>x.Sube).ToList()
            };

            return new SuccessDataResult<UserTokenDTO>(kullaniciToken, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[AuditAspect]
        //[SecuredOperation("KULLANICI_LISTELE")]
        public async Task<IDataResult<SayfalamaDTO<UserDTO>>> GetUserPagingAsync(UserCO co)
        {
            var list = await _userRepository.GetUserPagingAsync(co);
            return  new SuccessDataResult<SayfalamaDTO<UserDTO>>(list, @Resource.bilgilerBasariylaGonderilmistir);
        }


        [AuditAspect]
        [SecuredOperation("KULLANICI_DUZENLE")]
        public async Task<IDataResult<UserDTO>> GetByGuid(Guid kullaniciGuid)
        {

            var dto = await _userRepository.GetByGuidAsync(kullaniciGuid);
            if (dto?.Guid == null)
                return new SuccessDataResult<UserDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
            //dto.Ad = _cryptoHelper.Decrypt(dto.Ad);
            //dto.KullaniciAdi = _cryptoHelper.Decrypt(dto.KullaniciAdi);
            //dto.Sifre = _cryptoHelper.Decrypt(dto.Sifre);
            return new SuccessDataResult<UserDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        [AuditAspect]
        [SecuredOperation("KULLANICI_DUZENLE")]
        public async Task<IDataResult<UserDTO>> GetById(int kullaniciId)
        {
            var dto = await _userRepository.GetByIdAsync(kullaniciId);
            return new SuccessDataResult<UserDTO>(dto, @Resource.bilgilerBasariylaGonderilmistir);
        }

        //[AuditAspect]
        //[SecuredOperation("KULLANICI_EKLE")]
        public async Task<IDataResult<UserDTO>> AddUserAsync(UserRequest request)
        {
            try
            {
                request.UserName = _protectHelper.XssProtect(request.UserName);

                if (string.IsNullOrEmpty(request.UserName))
                {
                    return new ErrorDataResult<UserDTO>(null, "Kullanıcı adı boş olamaz!");
                }

                if (string.IsNullOrEmpty(request.Name))
                {
                    return new ErrorDataResult<UserDTO>(null, "Ad boş olamaz!");
                }

                if (string.IsNullOrEmpty(request.Surname))
                {
                    return new ErrorDataResult<UserDTO>(null, "Soyad boş olamaz!");
                }

                if (string.IsNullOrEmpty(request.Password))
                {
                    return new ErrorDataResult<UserDTO>(null, "Şifre boş olamaz!");
                }


                if (!string.IsNullOrEmpty(request.UserName))
                {
                    request.UserName = request.UserName.ToLower(new CultureInfo("en-US", false));
                }
                var kullaniciVarMi = await _userRepository.GetUserLoginAsync(request.UserName);
                if (kullaniciVarMi != null)
                {
                    return new ErrorDataResult<UserDTO>(null, "Böyle bir kullanıcı adı zaten bulunmaktadır!");
                }

                var tuzlamaDegeri = HashingHelper.CreatePasswordSalt();
                var sifreHash = HashingHelper.CreatePasswordHash(request.Password, tuzlamaDegeri);

                var kullaniciDto = new UserDTO()
                {
                    UserName = request.UserName,
                    Name = request.Name,
                    Surname = request.Surname,
                    IsActive = request.IsActive,
                    EMail = request.EMail,
                    Telephone = request.Telephone,
                    PasswordAgain = sifreHash,
                    Password = sifreHash,
                    TuzlamaDegeri = tuzlamaDegeri,
                    Guid = Guid.NewGuid(),
                    CreationTime = DateTime.Now
                };

                var addId = await _userRepository.CreateAsync(kullaniciDto);
                kullaniciDto.Id = addId;

                return new SuccessDataResult<UserDTO>(kullaniciDto, @Resource.kayitBasariylaEklendi);
            }
            catch (Exception ex)
            {

                throw;
            }
            return new ErrorDataResult<UserDTO>( @Resource.kayitEklenemedi);

        }


        [AuditAspect]
        [SecuredOperation("KULLANICI_DUZENLE")]
        public async Task<IDataResult<UserDTO>> UpdateUserAsync(Guid kullaniciGuid, UserRequest request)
        {
            request.UserName = _protectHelper.XssProtect(request.UserName);

            if (string.IsNullOrEmpty(request.UserName))
            {
                return new ErrorDataResult<UserDTO>(null, "Kullanıcı adı boş olamaz!");
            }


            if (string.IsNullOrEmpty(request.Name))
            {
                return new ErrorDataResult<UserDTO>(null, "Ad boş olamaz!");
            }

            if (string.IsNullOrEmpty(request.Surname))
            {
                return new ErrorDataResult<UserDTO>(null, "Soyad boş olamaz!");
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                request.UserName = request.UserName.ToLower(new CultureInfo("en-US", false));
            }

            var entity = await _userRepository.GetByGuidAsync(kullaniciGuid);

            var kullaniciVarMi = await _userRepository.GetUserPagingAsync(new UserCO()
            {
                UserName = request.UserName,
                SayfaNo = 0,
                SayfaAdet = int.MaxValue
            });

            if (kullaniciVarMi != null && kullaniciVarMi.Veri != null &&
                kullaniciVarMi.Veri.Any(x => x.Id != entity.Id && x.UserName == request.UserName))
            {
                return new ErrorDataResult<UserDTO>(null,
                    "Kullanıcı adı başka bir kullanıcı tarafından kullanılmaktadır!");
            }


            var tuzlamaDegeri = entity.TuzlamaDegeri;
            var sifreHash = entity.Password;
            if (!string.IsNullOrEmpty(request.Password))
            {
                tuzlamaDegeri = HashingHelper.CreatePasswordSalt();
                sifreHash = HashingHelper.CreatePasswordHash(request.Password, tuzlamaDegeri);
            }


            entity.UserName = request.UserName;
            entity.Name = request.Name;
            entity.Surname = request.Surname;
            entity.IsActive = request.IsActive;
            entity.EMail = request.EMail;
            entity.PasswordAgain = sifreHash;
            entity.Password = sifreHash;
            entity.TuzlamaDegeri = tuzlamaDegeri;
            //entity.GuncellenmeZamani = DateTime.Now;


            //entity.KasaId = request.KasaId;
            //entity.KasaAdi = request.KasaAdi;
            //entity.SunucuVeritabaniId = request.SunucuVeritabaniId;
            var updatedId = await _userRepository.UpdateAsync(entity);

            return new SuccessDataResult<UserDTO>(entity, @Resource.kayitBasariylaGuncellendi);
        }



        [AuditAspect]
        [SecuredOperation("KULLANICI_SIL")]
        public async Task<IResult> DeleteUserAsync(Guid kullaniciGuid)
        {
            var data = await _userRepository.GetByGuidAsync(kullaniciGuid);
            if (data == null)
            {
                return new ErrorResult("Kayıt bulunamadı!");
            }
            await _userRepository.DeleteAsync(kullaniciGuid);
            return new SuccessResult(@Resource.kayitBasariylaSilindi);
        }

        #region Sifre
        //public async Task<IResult> SifremiUnuttum(SifremiUnuttumCO co)
        //{
        //    if (!string.IsNullOrEmpty(co.KullaniciAdi))
        //    {
        //        co.KullaniciAdi = co.KullaniciAdi.ToLower(new CultureInfo("en-US", false)).Trim();
        //    }
        //    else
        //    {
        //        return new ErrorResult("Kullanıcı adınızı giriniz!");
        //    }
        //    var kullanici = await _userRepository.GetKullaniciGirisAsync(co.KullaniciAdi);
        //    if (kullanici == null)
        //    {
        //        return new ErrorResult("Kullanıcı bulunamadı!");
        //    }

        //    if (string.IsNullOrEmpty(kullanici.Eposta) && string.IsNullOrEmpty(kullanici.Eposta2))
        //    {
        //        return new ErrorResult("Kullanıcıya ait e-posta adresi tanımlı değildir!");
        //    }

        //    Random random = new Random();  

        //   kullanici.Kod = random.Next(0, 10) + "" + random.Next(0, 10) + "" + random.Next(0, 10) + "" +
        //                   random.Next(0, 10) + "" + random.Next(0, 10) + "" + random.Next(0, 10);
        //   kullanici.KodGirisSayisi = 0;
        //   kullanici.KodSonGirisTarihi = DateTime.Now.AddDays(1);
        //   var guncelleId = await _userRepository.UpdateAsync(kullanici);
        //   if (guncelleId > 0)
        //   {
        //       var mailBody = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "Templates/SifremiUnuttum.html"));

        //       var epostaAyar = await this._epostaService.GetEpostaByTipAsync(((int)EEpostaTuru.Genel).ToString());
        //       if (epostaAyar == null || epostaAyar.Data == null)
        //       {
        //           return new ErrorResult("E-posta sunucu ayarları bulunamadı!");
        //       }

        //       var eposta = kullanici.Eposta;
        //       if (string.IsNullOrEmpty(eposta))
        //       {
        //           eposta = kullanici.Eposta2;
        //       }

        //       mailBody = mailBody.Replace("{#Adi#}", kullanici.Ad);
        //       mailBody = mailBody.Replace("{#Soyadi#}", kullanici.Soyad);
        //       mailBody = mailBody.Replace("{#Kod#}", kullanici.Kod);
        //       var epostaBilgi = new EpostaBilgi()
        //       {
        //           Ad = kullanici.Ad,
        //           Soyad = kullanici.Soyad,
        //           EkleyenId = kullanici.Id,
        //           Eposta = eposta,
        //           EpostaKonu = "Şifrenizi mi Unuttunuz?",
        //           EpostaIcerik = mailBody
        //       };

        //    var epostaGonder  =  await _epostaService.EpostaGonderAsync(epostaAyar.Data, epostaBilgi, "SIFRE");
        //    if (epostaGonder.Data)
        //    {
        //        return new SuccessResult($"Şifre değiştirme kodu {eposta} e-posta adresinize başarıyla gönderilmiştir.");
        //    }
        //   }
        //   else
        //   {
        //       return new ErrorResult("Şifremi unuttum ayarları yapılamadı!");
        //   }

        //    return new ErrorResult("Şifre yenileme kodu gönderilemedi!");

        //}

        //public async Task<IResult> SifreYenile(SifreYenileCO co)
        //{
        //    if (!string.IsNullOrEmpty(co.KullaniciAdi))
        //    {
        //        co.KullaniciAdi = co.KullaniciAdi.ToLower(new CultureInfo("en-US", false)).Trim();
        //    }
        //    else
        //    {
        //        return new ErrorResult("Kullanıcı adınızı giriniz!");
        //    }

        //    var kullanici = await _userRepository.GetKullaniciGirisAsync(co.KullaniciAdi);
        //    if (kullanici == null)
        //    {
        //        return new ErrorResult("Kullanıcı bulunamadı!");
        //    }

        //    if (string.IsNullOrEmpty(kullanici.Kod))
        //    {
        //        return new ErrorResult("Kullanıcıya ait şifre yenileme kodu tanımlı değildir!");
        //    }

        //    kullanici.KodGirisSayisi = kullanici.KodGirisSayisi.HasValue ? kullanici.KodGirisSayisi.Value + 1 : 1;
        //    if (kullanici.KodGirisSayisi > 3)
        //    {
        //        return new ErrorResult(
        //            "Kullanıcıya ait şifre yenileme kodu  artık kullanılamaz. Lütfen yeni şifre yenileme kodu alınız!");
        //    }

        //    await _userRepository.UpdateAsync(kullanici);

        //    if (!kullanici.KodSonGirisTarihi.HasValue)
        //    {

        //        return new ErrorResult(
        //            "Kullanıcıya ait şifre yenileme kodu son kullanım tarihi tanımlı olmadığından işlem yapılamıyor");
        //    }

        //    if (kullanici.KodSonGirisTarihi.Value < DateTime.Now)
        //    {
        //        return new ErrorResult("Şifre yenileme kodu son kullanım tarihi geçmiştir!");
        //    }

        //    if (string.IsNullOrEmpty(co.Kod))
        //    {
        //        return new ErrorResult("Şifre yenileme kodu boş olamaz!");
        //    }

        //    if (string.IsNullOrEmpty(co.Sifre))
        //    {
        //        return new ErrorResult("Şifre boş olamaz!");
        //    }

        //    if (kullanici.Kod != co.Kod)
        //    {
        //        return new ErrorResult("Şifre yenileme kodu hatalıdır!");
        //    }


        //    var tuzlamaDegeri = HashingHelper.CreatePasswordSalt();
        //    var sifreHash = HashingHelper.CreatePasswordHash(co.Sifre, tuzlamaDegeri);

        //    kullanici.TuzlamaDegeri = tuzlamaDegeri;
        //    kullanici.Sifre = sifreHash;

        //    var guncelleId = await _userRepository.UpdateAsync(kullanici);
        //    if (guncelleId > 0)
        //    {
        //        return new SuccessResult("Şifre başarıyla değiştirildi!");
        //    }

        //    return new ErrorResult("Şifre değiştirilemedi!");
        //}
        #endregion

    }
}
