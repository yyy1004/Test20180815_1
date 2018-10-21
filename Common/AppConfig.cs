using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Reflection;


namespace JrscSoft.Common
{

    public enum ConfigFileType
    {
        WebConfig,
        AppConfig
    }

    public class AppConfig
    {
        public AppConfig()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        public string docName = String.Empty;
        private XmlNode node = null;

        private int _configType;
        public int ConfigType
        {
            get
            {
                return _configType;
            }
            set
            {
                _configType = value;
            }
        }

        public bool SetValue(string key, string value)
        {
            XmlDocument cfgDoc = new XmlDocument();
            loadConfigDoc(cfgDoc);
            // retrieve the appSettings node 
            node = cfgDoc.SelectSingleNode("//appSettings");

            if (node == null)
            {
                throw new System.InvalidOperationException("appSettings section not found");
            }

            try
            {
                // XPath select setting "add" element that contains this key    
                XmlElement addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
                if (addElem != null)
                {
                    addElem.SetAttribute("value", value);
                }
                // not found, so we need to add the element, key and value
                else
                {
                    XmlElement entry = cfgDoc.CreateElement("add");
                    entry.SetAttribute("key", key);
                    entry.SetAttribute("value", value);
                    node.AppendChild(entry);
                }
                //save it
                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        public bool removeElement(string elementKey)
        {
            try
            {
                XmlDocument cfgDoc = new XmlDocument();
                loadConfigDoc(cfgDoc);
                // retrieve the appSettings node 
                node = cfgDoc.SelectSingleNode("//appSettings");
                if (node == null)
                {
                    throw new System.InvalidOperationException("appSettings section not found");
                }
                // XPath select setting "add" element that contains this key to remove   
                node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));

                saveConfigDoc(cfgDoc, docName);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public XmlDocument loadConfigDoc(XmlDocument cfgDoc)
        {
            // load the config file 
            if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
            {

                docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
                docName += ".exe.config";
            }
            else
            {
                docName = System.AppDomain.CurrentDomain.BaseDirectory + "web.config";              //在类库中获得网站物理地址
                //docName = System.Web.HttpContext.Current.Server.MapPath("../web.config");         //在页面上获得网站物理地址
            }
            cfgDoc.Load(docName);
            return cfgDoc;
        }

    }
}
