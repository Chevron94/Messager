using System.IO;
using System.Net;

namespace Interpals
{
    public static class Http
    {
        public static string SteamWebRequest(string url, string post = null, string referer = null, CookieContainer cookie = null )
        {
            HttpWebResponse httpWebResponse = null;
            StreamReader streamReader = null;
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                
                httpWebRequest.Method = ((post != null) ? "POST" : "GET");
                httpWebRequest.Accept = ((post != null) ? "*/*" : "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                if (post != null)
                {
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                }
                httpWebRequest.CookieContainer = cookie;
                httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
                if (post != null)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(post);
                    }
                }
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    try
                    {
                        streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                        return streamReader.ReadToEnd();
                    }catch(IOException e)
                    {
                        if (streamReader != null)
                        {
                            streamReader.Close();
                        }
                        if (httpWebResponse != null)
                        {
                            httpWebResponse.Close();
                        }
                        return null;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    var httpWebResponse2 = (HttpWebResponse)ex.Response;
                    //return new StreamReader(httpWebResponse2.GetResponseStream()).ReadToEnd();
                    //throw ex;
                    return null;
                }
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }
            }
            return null;
        }
    }
}