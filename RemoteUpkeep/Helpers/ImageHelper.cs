using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RemoteUpkeep.Helpers
{
    public static class ImageHelper
    {
        public static byte[] GetImageResized(byte[] src, int width, int height, ImageFormat ImageFormat)
        {
            if (width == 0 && height == 0)
                return src;

            MemoryStream s = new MemoryStream(src, 0, src.Length);
            Bitmap srcBitmap = new Bitmap(s);

            if (width > 0 && height > 0)
            {
                if ((decimal)srcBitmap.Width / srcBitmap.Height > (decimal)width / height)
                {
                    height = (int)(width * (decimal)srcBitmap.Height / srcBitmap.Width);
                }
                else
                {
                    width = (int)(height * (decimal)srcBitmap.Width / srcBitmap.Height);
                }
            }
            else if (width > 0)
            {
                height = (int)(width * (decimal)srcBitmap.Height / srcBitmap.Width);
            }
            else if (height > 0)
            {
                width = (int)(height * (decimal)srcBitmap.Width / srcBitmap.Height);
            }

            Bitmap res = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(res))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(srcBitmap, 0, 0, width, height);
            }

            MemoryStream ms = new MemoryStream();
            res.Save(ms, ImageFormat);

            return ms.ToArray();
        }

        public static byte[] GetImageResized(byte[] src, int width, int height)
        {
            return GetImageResized(src, width, height, ImageFormat.Jpeg);
        }

        public static byte[] GetImageCroped(byte[] src, int width, int height, bool center, ImageFormat ImageFormat)
        {
            if (width == 0 && height == 0)
                return src;

            MemoryStream s = new MemoryStream(src, 0, src.Length);
            Bitmap srcBitmap = new Bitmap(s);

            Bitmap res = new Bitmap(width, height);

            int x = 0;
            int y = 0;

            if (width > 0 && height > 0)
            {
                if ((decimal)srcBitmap.Width / srcBitmap.Height > (decimal)width / height)
                {
                    int widthOver = (int)(height * (decimal)srcBitmap.Width / srcBitmap.Height);
                    x = (widthOver - width) / 2;
                    width = widthOver;
                }
                else
                {
                    int heightOver = (int)(width * (decimal)srcBitmap.Height / srcBitmap.Width);
                    y = (heightOver - height) / 2;
                    height = heightOver;
                }
            }

            using (Graphics g = Graphics.FromImage(res))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(srcBitmap, (center ? -x : 0), (center ? -y : 0), width, height);
            }

            MemoryStream ms = new MemoryStream();
            res.Save(ms, ImageFormat);

            return ms.ToArray();
        }

        public static byte[] GetImageCroped(byte[] src, int width, int height, bool center)
        {
            return GetImageCroped(src, width, height, center, ImageFormat.Jpeg);
        }

        public static string GetContentType(string Extension)
        {
            if (Extension == null)
                return null;

            if (Extension.ToLower() == ".epub") return "application/epub+zip";
            if (Extension.ToLower() == ".fif") return "application/fractals";
            if (Extension.ToLower() == ".spl") return "application/futuresplash";
            if (Extension.ToLower() == ".hta") return "application/hta";
            if (Extension.ToLower() == ".kml") return "application/keyhole";
            if (Extension.ToLower() == ".hqx") return "application/mac-binhex40";
            if (Extension.ToLower() == ".infopathxml") return "application/ms-infopath.xml";
            if (Extension.ToLower() == ".vsi") return "application/ms-vsi";
            if (Extension.ToLower() == ".accdb") return "application/msaccess";
            if (Extension.ToLower() == ".doc") return "application/msword";
            if (Extension.ToLower() == ".pdf") return "application/pdf";
            if (Extension.ToLower() == ".p10") return "application/pkcs10";
            if (Extension.ToLower() == ".p7m") return "application/pkcs7-mime";
            if (Extension.ToLower() == ".p7s") return "application/pkcs7-signature";
            if (Extension.ToLower() == ".cer") return "application/pkix-cert";
            if (Extension.ToLower() == ".crl") return "application/pkix-crl";
            if (Extension.ToLower() == ".ps") return "application/postscript";
            if (Extension.ToLower() == ".sdp") return "application/sdp";
            if (Extension.ToLower() == ".setpay") return "application/set-payment-initiation";
            if (Extension.ToLower() == ".setreg") return "application/set-registration-initiation";
            if (Extension.ToLower() == ".acrobatsecuritysettings") return "application/vnd.adobe.acrobat-security-settings";
            if (Extension.ToLower() == ".acsm") return "application/vnd.adobe.adept+xml";
            if (Extension.ToLower() == ".pdfxml") return "application/vnd.adobe.pdfxml";
            if (Extension.ToLower() == ".pdx") return "application/vnd.adobe.pdx";
            if (Extension.ToLower() == ".xdp") return "application/vnd.adobe.xdp+xml";
            if (Extension.ToLower() == ".xfd") return "application/vnd.adobe.xfd+xml";
            if (Extension.ToLower() == ".xfdf") return "application/vnd.adobe.xfdf";
            if (Extension.ToLower() == ".fdf") return "application/vnd.fdf";
            if (Extension.ToLower() == ".kml") return "application/vnd.google-earth.kml+xml";
            if (Extension.ToLower() == ".kmz") return "application/vnd.google-earth.kmz";
            if (Extension.ToLower() == ".xls") return "application/vnd.ms-excel";
            if (Extension.ToLower() == ".xlam") return "application/vnd.ms-excel.addin.macroEnabled.12";
            if (Extension.ToLower() == ".xlsb") return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
            if (Extension.ToLower() == ".xlsm") return "application/vnd.ms-excel.sheet.macroEnabled.12";
            if (Extension.ToLower() == ".xltm") return "application/vnd.ms-excel.template.macroEnabled.12";
            if (Extension.ToLower() == ".mpf") return "application/vnd.ms-mediapackage";
            if (Extension.ToLower() == ".thmx") return "application/vnd.ms-officetheme";
            if (Extension.ToLower() == ".rels") return "application/vnd.ms-package.relationships+xml";
            if (Extension.ToLower() == ".sst") return "application/vnd.ms-pki.certstore";
            if (Extension.ToLower() == ".pko") return "application/vnd.ms-pki.pko";
            if (Extension.ToLower() == ".cat") return "application/vnd.ms-pki.seccat";
            if (Extension.ToLower() == ".stl") return "application/vnd.ms-pki.stl";
            if (Extension.ToLower() == ".ppt") return "application/vnd.ms-powerpoint";
            if (Extension.ToLower() == ".ppam") return "application/vnd.ms-powerpoint.addin.macroEnabled.12";
            if (Extension.ToLower() == ".pptm") return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
            if (Extension.ToLower() == ".sldm") return "application/vnd.ms-powerpoint.slide.macroEnabled.12";
            if (Extension.ToLower() == ".ppsm") return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";
            if (Extension.ToLower() == ".potm") return "application/vnd.ms-powerpoint.template.macroEnabled.12";
            if (Extension.ToLower() == ".pub") return "application/vnd.ms-publisher";
            if (Extension.ToLower() == ".vdx") return "application/vnd.ms-visio.viewer";
            if (Extension.ToLower() == ".docm") return "application/vnd.ms-word.document.macroEnabled.12";
            if (Extension.ToLower() == ".dotm") return "application/vnd.ms-word.template.macroEnabled.12";
            if (Extension.ToLower() == ".wpl") return "application/vnd.ms-wpl";
            if (Extension.ToLower() == ".xps") return "application/vnd.ms-xpsdocument";
            if (Extension.ToLower() == ".odp") return "application/vnd.oasis.opendocument.presentation";
            if (Extension.ToLower() == ".ods") return "application/vnd.oasis.opendocument.spreadsheet";
            if (Extension.ToLower() == ".odt") return "application/vnd.oasis.opendocument.text";
            if (Extension.ToLower() == ".pptx") return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
            if (Extension.ToLower() == ".sldx") return "application/vnd.openxmlformats-officedocument.presentationml.slide";
            if (Extension.ToLower() == ".ppsx") return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
            if (Extension.ToLower() == ".potx") return "application/vnd.openxmlformats-officedocument.presentationml.template";
            if (Extension.ToLower() == ".xlsx") return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            if (Extension.ToLower() == ".xltx") return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
            if (Extension.ToLower() == ".docx") return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            if (Extension.ToLower() == ".dotx") return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
            if (Extension.ToLower() == ".z") return "application/x-compress";
            if (Extension.ToLower() == ".tgz") return "application/x-compressed";
            if (Extension.ToLower() == ".gz") return "application/x-gzip";
            if (Extension.ToLower() == ".uin") return "application/x-icq";
            if (Extension.ToLower() == ".ins") return "application/x-internet-signup";
            if (Extension.ToLower() == ".iii") return "application/x-iphone";
            if (Extension.ToLower() == ".jnlp") return "application/x-java-jnlp-file";
            if (Extension.ToLower() == ".jtx") return "application/x-jtx+xps";
            if (Extension.ToLower() == ".latex") return "application/x-latex";
            if (Extension.ToLower() == ".nix") return "application/x-mix-transfer";
            if (Extension.ToLower() == ".amc") return "application/x-mpeg";
            if (Extension.ToLower() == ".asx") return "application/x-mplayer2";
            if (Extension.ToLower() == ".application") return "application/x-ms-application";
            if (Extension.ToLower() == ".vsto") return "application/x-ms-vsto";
            if (Extension.ToLower() == ".wmd") return "application/x-ms-wmd";
            if (Extension.ToLower() == ".wmz") return "application/x-ms-wmz";
            if (Extension.ToLower() == ".xbap") return "application/x-ms-xbap";
            if (Extension.ToLower() == ".xls") return "application/x-msexcel";
            if (Extension.ToLower() == ".ppt") return "application/x-mspowerpoint";
            if (Extension.ToLower() == ".pptx") return "application/x-mspowerpoint.12";
            if (Extension.ToLower() == ".pptm") return "application/x-mspowerpoint.macroEnabled.12";
            if (Extension.ToLower() == ".p12") return "application/x-pkcs12";
            if (Extension.ToLower() == ".p7b") return "application/x-pkcs7-certificates";
            if (Extension.ToLower() == ".p7r") return "application/x-pkcs7-certreqresp";
            if (Extension.ToLower() == ".qtl") return "application/x-quicktimeplayer";
            if (Extension.ToLower() == ".rtsp") return "application/x-rtsp";
            if (Extension.ToLower() == ".sdp") return "application/x-sdp";
            if (Extension.ToLower() == ".swf") return "application/x-shockwave-flash";
            if (Extension.ToLower() == ".skype") return "application/x-skype";
            if (Extension.ToLower() == ".sparc") return "application/x-sparc";
            if (Extension.ToLower() == ".sit") return "application/x-stuffit";
            if (Extension.ToLower() == ".tar") return "application/x-tar";
            if (Extension.ToLower() == ".man") return "application/x-troff-man";
            if (Extension.ToLower() == ".wlpginstall") return "application/x-wlpg-detect";
            if (Extension.ToLower() == ".wlpginstall3") return "application/x-wlpg3-detect";
            if (Extension.ToLower() == ".cer") return "application/x-x509-ca-cert";
            if (Extension.ToLower() == ".zip") return "application/x-zip-compressed";
            if (Extension.ToLower() == ".xaml") return "application/xaml+xml";
            if (Extension.ToLower() == ".xej") return "application/xej+xml";
            if (Extension.ToLower() == ".xel") return "application/xel+xml";
            if (Extension.ToLower() == ".xml") return "application/xml";
            if (Extension.ToLower() == ".3gp") return "audio/3gpp";
            if (Extension.ToLower() == ".3g2") return "audio/3gpp2";
            if (Extension.ToLower() == ".aac") return "audio/aac";
            if (Extension.ToLower() == ".ac3") return "audio/ac3";
            if (Extension.ToLower() == ".aiff") return "audio/aiff";
            if (Extension.ToLower() == ".amr") return "audio/AMR";
            if (Extension.ToLower() == ".au") return "audio/basic";
            if (Extension.ToLower() == ".mid") return "audio/mid";
            if (Extension.ToLower() == ".mid") return "audio/midi";
            if (Extension.ToLower() == ".mp3") return "audio/mp3";
            if (Extension.ToLower() == ".mp4") return "audio/mp4";
            if (Extension.ToLower() == ".mp3") return "audio/mpeg";
            if (Extension.ToLower() == ".m3u") return "audio/mpegurl";
            if (Extension.ToLower() == ".mp3") return "audio/mpg";
            if (Extension.ToLower() == ".qcp") return "audio/vnd.qcelp";
            if (Extension.ToLower() == ".wav") return "audio/wav";
            if (Extension.ToLower() == ".aac") return "audio/x-aac";
            if (Extension.ToLower() == ".ac3") return "audio/x-ac3";
            if (Extension.ToLower() == ".aiff") return "audio/x-aiff";
            if (Extension.ToLower() == ".caf") return "audio/x-caf";
            if (Extension.ToLower() == ".gsm") return "audio/x-gsm";
            if (Extension.ToLower() == ".m4a") return "audio/x-m4a";
            if (Extension.ToLower() == ".m4b") return "audio/x-m4b";
            if (Extension.ToLower() == ".m4p") return "audio/x-m4p";
            if (Extension.ToLower() == ".mid") return "audio/x-mid";
            if (Extension.ToLower() == ".mid") return "audio/x-midi";
            if (Extension.ToLower() == ".mp3") return "audio/x-mp3";
            if (Extension.ToLower() == ".mp3") return "audio/x-mpeg";
            if (Extension.ToLower() == ".m3u") return "audio/x-mpegurl";
            if (Extension.ToLower() == ".mp3") return "audio/x-mpg";
            if (Extension.ToLower() == ".wax") return "audio/x-ms-wax";
            if (Extension.ToLower() == ".wma") return "audio/x-ms-wma";
            if (Extension.ToLower() == ".wav") return "audio/x-wav";
            if (Extension.ToLower() == ".bmp") return "image/bmp";
            if (Extension.ToLower() == ".design") return "image/design";
            if (Extension.ToLower() == ".gif") return "image/gif";
            if (Extension.ToLower() == ".jp2") return "image/jp2";
            if (Extension.ToLower() == ".jpg") return "image/jpeg";
            if (Extension.ToLower() == ".jp2") return "image/jpeg2000";
            if (Extension.ToLower() == ".jp2") return "image/jpeg2000-image";
            if (Extension.ToLower() == ".pict") return "image/pict";
            if (Extension.ToLower() == ".jpg") return "image/pjpeg";
            if (Extension.ToLower() == ".png") return "image/png";
            if (Extension.ToLower() == ".tiff") return "image/tiff";
            if (Extension.ToLower() == ".mdi") return "image/vnd.ms-modi";
            if (Extension.ToLower() == ".ico") return "image/x-icon";
            if (Extension.ToLower() == ".jp2") return "image/x-jpeg2000-image";
            if (Extension.ToLower() == ".pntg") return "image/x-macpaint";
            if (Extension.ToLower() == ".pict") return "image/x-pict";
            if (Extension.ToLower() == ".png") return "image/x-png";
            if (Extension.ToLower() == ".qtif") return "image/x-quicktime";
            if (Extension.ToLower() == ".sgi") return "image/x-sgi";
            if (Extension.ToLower() == ".targa") return "image/x-targa";
            if (Extension.ToLower() == ".xpr") return "image/xpr";
            if (Extension.ToLower() == ".mid") return "midi/mid";
            if (Extension.ToLower() == ".dwfx") return "model/vnd.dwfx+xps";
            if (Extension.ToLower() == ".ics") return "text/calendar";
            if (Extension.ToLower() == ".css") return "text/css";
            if (Extension.ToLower() == ".dlm") return "text/dlm";
            if (Extension.ToLower() == ".323") return "text/h323";
            if (Extension.ToLower() == ".htm") return "text/html";
            if (Extension.ToLower() == ".uls") return "text/iuls";
            if (Extension.ToLower() == ".txt") return "text/plain";
            if (Extension.ToLower() == ".wsc") return "text/scriptlet";
            if (Extension.ToLower() == ".htt") return "text/webviewhtml";
            if (Extension.ToLower() == ".htc") return "text/x-component";
            if (Extension.ToLower() == ".iqy") return "text/x-ms-iqy";
            if (Extension.ToLower() == ".odc") return "text/x-ms-odc";
            if (Extension.ToLower() == ".rqy") return "text/x-ms-rqy";
            if (Extension.ToLower() == ".vcf") return "text/x-vcard";
            if (Extension.ToLower() == ".xml") return "text/xml";
            if (Extension.ToLower() == ".3gp") return "video/3gpp";
            if (Extension.ToLower() == ".3g2") return "video/3gpp2";
            if (Extension.ToLower() == ".avi") return "video/avi";
            if (Extension.ToLower() == ".flc") return "video/flc";
            if (Extension.ToLower() == ".mp4") return "video/mp4";
            if (Extension.ToLower() == ".mpg") return "video/mpeg";
            if (Extension.ToLower() == ".mpg") return "video/mpg";
            if (Extension.ToLower() == ".avi") return "video/msvideo";
            if (Extension.ToLower() == ".mov") return "video/quicktime";
            if (Extension.ToLower() == ".sdv") return "video/sd-video";
            if (Extension.ToLower() == ".m4v") return "video/x-m4v";
            if (Extension.ToLower() == ".mpg") return "video/x-mpeg";
            if (Extension.ToLower() == ".mpg") return "video/x-mpeg2a";
            if (Extension.ToLower() == ".asx") return "video/x-ms-asf";
            if (Extension.ToLower() == ".asx") return "video/x-ms-asf-plugin";
            if (Extension.ToLower() == ".wm") return "video/x-ms-wm";
            if (Extension.ToLower() == ".wmv") return "video/x-ms-wmv";
            if (Extension.ToLower() == ".wmx") return "video/x-ms-wmx";
            if (Extension.ToLower() == ".wvx") return "video/x-ms-wvx";
            if (Extension.ToLower() == ".avi") return "video/x-msvideo";
            if (Extension.ToLower() == ".flv") return "video/x-flv";

            return null;
        }
    }
}
