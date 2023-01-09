using System.Collections.Generic;

namespace ECommerce.Core.Utilities.Security.Request
{
    public class ProtectHelper
    {
        List<string> _guvenlikListe = new List<string>();

        public string DegerTemizle(string input)
        {
            string data = input.Trim();
            data = data.Replace("&gt;", "");
            data = data.Replace("&lt;", "");
            data = data.Replace(">", "");
            data = data.Replace("<", "");
            data = data.Replace("--", "");
            data = data.Replace("'", "");
            data = data.Replace(";", "");
            data = data.Replace("=", "");
            data = data.Replace("char ", "");
            data = data.Replace("delete ", "");
            data = data.Replace("insert ", "");
            data = data.Replace("update ", "");
            data = data.Replace("select ", "");
            data = data.Replace("truncate ", "");
            data = data.Replace("union", "");
            data = data.Replace("script ", "");
            data = data.Replace("$", "");
            data = data.Replace("echo", "");
            data = data.Replace("(", "");
            data = data.Replace(")", "");
            data = data.Replace(" ", "");
            data = data.Replace("wait for delay", "");
            return data;
        }

        public string XssProtect(string input, bool boslukTemizle = true)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string data = boslukTemizle ? input.Trim() : input;
            data = data.Replace("char ", "");
            data = data.Replace("delete ", "");
            data = data.Replace("insert ", "");
            data = data.Replace("update ", "");
            data = data.Replace("select ", "");
            data = data.Replace("truncate ", "");
            data = data.Replace("union", "");
            data = data.Replace("<script>", "");
            data = data.Replace("</script>", "");
            data = data.Replace("XMLHttpRequest()", "");
            data = data.Replace(".open", "");
            data = data.Replace(".setRequestHeader", "");
            data = data.Replace(".send", "");
            data = data.Replace("')", "");
            data = data.Replace("('", "");
            data = data.Replace("alert", "");
            data = data.Replace("ALERT", "");
            data = data.Replace("document.cookie", "");
            data = data.Replace("Image()", "");
            data = data.Replace("output", "");
            data = data.Replace("'post'", "");
            data = data.Replace(".js", "");
            data = data.Replace("javascript", "");
            data = data.Replace("<link>", "");
            data = data.Replace("</link>", "");
            data = data.Replace("<iframe>", "");
            data = data.Replace("</iframe>", "");
            data = data.Replace("();", "");
            data = data.Replace("<html>", "");
            data = data.Replace("</html>", "");
            data = data.Replace("function", "");
            data = data.Replace("<context-param>", "");
            data = data.Replace("</context-param>", "");
            data = data.Replace("<filter>", "");
            data = data.Replace("</filter>", "");
            data = data.Replace("getElementById", "");
            data = data.Replace(".value", "");
            data = data.Replace("response.redirect", "");
            data = data.Replace("window.location.href", "");
            data = data.Replace("(\"", "");
            data = data.Replace("\")", "");
            data = data.Replace("PostBackUrl", "");
            data = data.Replace("\n", "");
            data = data.Replace("\r", "");

            return data;
        }


        public bool GKontrol(string gelenVeri)
        {
            bool durum = false;

            _guvenlikListe.Add("char");
            _guvenlikListe.Add("delete");
            _guvenlikListe.Add("insert");
            _guvenlikListe.Add("update");
            _guvenlikListe.Add("select");
            _guvenlikListe.Add("truncate");
            _guvenlikListe.Add("union");
            _guvenlikListe.Add("<script>");
            _guvenlikListe.Add("</script>");
            _guvenlikListe.Add("XMLHttpRequest()");
            _guvenlikListe.Add(".open");
            _guvenlikListe.Add(".setRequestHeader");
            _guvenlikListe.Add(".send");
            _guvenlikListe.Add("')");
            _guvenlikListe.Add("('");
            _guvenlikListe.Add("alert");
            _guvenlikListe.Add("ALERT");
            _guvenlikListe.Add("document.cookie");
            _guvenlikListe.Add("Image()");
            _guvenlikListe.Add("output");
            _guvenlikListe.Add("'post'");
            _guvenlikListe.Add(".js");
            _guvenlikListe.Add("javascript");
            _guvenlikListe.Add("<link>");
            _guvenlikListe.Add("</link>");
            _guvenlikListe.Add("<iframe>");
            _guvenlikListe.Add("</iframe>");
            _guvenlikListe.Add("();");
            _guvenlikListe.Add("function");
            _guvenlikListe.Add("<context-param>");
            _guvenlikListe.Add("</context-param>");
            _guvenlikListe.Add("<filter>");
            _guvenlikListe.Add("</filter>");
            _guvenlikListe.Add("getElementById");
            _guvenlikListe.Add(".value");
            _guvenlikListe.Add("response.redirect");
            _guvenlikListe.Add("window.location.href");
            _guvenlikListe.Add("(\"");
            _guvenlikListe.Add("\")");
            _guvenlikListe.Add("PostBackUrl");
            _guvenlikListe.Add("\n");
            _guvenlikListe.Add("\r");


            for (int e = 0; e < _guvenlikListe.Count; e++)
            {
                string kontrol = _guvenlikListe[e];
                if (gelenVeri.Contains(kontrol))
                {
                    durum = true;
                    break;
                }
            }


            return durum;
        }
    }
}
