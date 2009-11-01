using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using Web.MVC.Model;

namespace horn_get
{
    public static class HornHttpClient
    {
        static T GetData<T>(string url)
        {
            HttpWebRequest request = SetupRequest(url);

            var response = request.GetResponse();

            var stream = new StreamReader(response.GetResponseStream());

            var data = stream.ReadToEnd();

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<T>(data);
        }

        static HttpWebRequest SetupRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Accept = "application/json";
            request.UserAgent = String.Format("horn-get Command Line Client version {0}", Globals.VERSION);
            return request;
        }

        public static IList<Category> ListCategories(string serverUrl)
        {
            return GetData<IList<Category>>(String.Format("{0}/{1}/",serverUrl, "packages"));
        }

        public static Category ListCategory(string serverUrl, string category)
        {
            return GetData<Category>(String.Format("{0}/{1}/{2}/", serverUrl, "packages", category));
        }

        public static Category ListPackage(string serverUrl, string category, string package)
        {
            return GetData<Category>(String.Format("{0}/{1}/{2}/{3}/", serverUrl, "packages", category, package));
        }

        public static Package PackageDescription(string serverUrl, string category, string package)
        {
            
            var packageRoot = package.Remove(package.LastIndexOf("-"), package.Length - package.LastIndexOf("-"));
            return GetData<Package>(String.Format("{0}/{1}/{2}/{3}/{4}", serverUrl, "packages", category, packageRoot, package));
        }

        public static void PackageDownload(string serverUrl, string category, string package, string output)
        {
            var packageRoot = package.Remove(package.LastIndexOf("-"), package.Length - package.LastIndexOf("-"));

            HttpWebRequest request = SetupRequest(String.Format("{0}/{1}/{2}/{3}/{4}", serverUrl, "packages", category, packageRoot, package));

            var response = request.GetResponse();

            if (response != null)
            {
                var stream = response.GetResponseStream();

                SaveFile(String.Format(@"{0}\{1}.zip", output, package), stream);
            }
        }

        static void SaveFile(string output, Stream remoteStream)
        {
            var localStream = File.Create(output);

            byte[] buffer = new byte[1024];
            int bytesRead;

            do
            {
                bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                localStream.Write(buffer, 0, bytesRead);

            } while (bytesRead > 0);
        }
    }
}