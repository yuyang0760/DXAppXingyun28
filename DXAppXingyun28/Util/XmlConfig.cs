using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DXAppXingyun28.Util
{
    class XmlConfig
    {
        XmlControl xmlControl = new XmlControl();
        public string filePath;
        public XmlConfig(string filePath)
        {
            this.filePath = filePath;
            // 读取文件
            xmlControl.LoadXmlFile(new System.IO.FileInfo(filePath));
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void Add(string name, string value)
        {

            // 定义节点
            XmlElement xmlElement = xmlControl.doc.CreateElement("element");
            xmlElement.SetAttribute("name", name);
            xmlElement.InnerText = value;
            // 添加节点
            xmlControl.root.AppendChild(xmlElement);
            // 保存
            xmlControl.Save(filePath);
        }
        /// <summary>
        /// 修改节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void ModModify(string name, string value)
        {
 
            // 查找节点
            XmlNode xmlNode = xmlControl.SelectSingleNode($"//element[@name=\"{name}\"]");
            if(xmlNode == null)
            {
                Add(name, value);
                return;
            }
            xmlNode.InnerText = value;
            // 保存
            xmlControl.Save(filePath);

        }
        public string Search(string name)
        {
 
            // 查找节点
            XmlNode xmlNode = xmlControl.SelectSingleNode($"//element[@name=\"{name}\"]");
            // 返回value
            if (xmlNode == null)
            {
                return "";
            }
            else
            {
                return xmlNode.InnerText;

            }


        }
        /// <summary>
        /// 所有节点
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> AllNode()
        {
            // 查找节点
             XmlNodeList  xmlNodeList = xmlControl.SelectNodes($"//element");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlNode item in xmlNodeList)
            {
               string name= ((XmlElement)item).GetAttribute("name");
                string value = item.InnerText;
                dic[name] = value;
            }
            return dic;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="name"></param>
        public void Delete(string name)
        {
 
            // 查找节点
            XmlNode xmlNode = xmlControl.SelectSingleNode($"//element[@name=\"{name}\"]");
            xmlNode.ParentNode.RemoveChild(xmlNode);
            // 保存
            xmlControl.Save(filePath);

        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="element"></param>
        public void DeleteAll(string element )
        {

            // 查找节点
            XmlNode xmlNode = xmlControl.SelectSingleNode($"/{element}");
            xmlNode.RemoveAll();
            // 保存
            xmlControl.Save(filePath);

        }


        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="name"></param>
        public void Delete(string element,string name)
        {

            // 查找节点
            XmlNode xmlNode = xmlControl.SelectSingleNode($"//{element}[@name=\"{name}\"]");
            xmlNode.ParentNode.RemoveChild(xmlNode);
            // 保存
            xmlControl.Save(filePath);

        }
    }
}
