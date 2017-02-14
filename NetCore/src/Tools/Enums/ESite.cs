using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Tools.Enums
{
    public class ESite
    {
        /// <summary>
        /// 属性类型
        /// </summary>
        public enum AttrType : byte
        {
            [Description("文本框")]
            Text = 1,
            [Description("下拉框")]
            Dropdownlist = 2,
            [Description("多选框")]
            CheckBox = 3
        }

        /// <summary>
        /// 货币种类
        /// </summary>
        public enum Currency : byte
        {

            [Description("Dollar")]
            Dollar = 1,
            [Description("EUR")]
            EUR = 2,
            [Description("HK")]
            HK = 3,
            [Description("RMB")]
            RMB = 4,
        }

        /// <summary>
        /// 付款方式
        /// </summary>
        public enum PayMent : byte
        {
            /// <summary>
            /// 电汇
            /// </summary>
            [Description("TT")]
            TT = 2,
            /// <summary>
            /// 信用证
            /// </summary>
            [Description("L/C")]
            LC = 3,
            /// <summary>
            /// 付款交单
            /// </summary>
            [Description("DP")]
            DP = 4,
            /// <summary>
            /// 放账
            /// </summary>
            [Description("OA")]
            OA = 5,
            /// <summary>
            /// 其他
            /// </summary>
            [Description("Other")]
            Other = 255
        }

        /// <summary>
        /// 交易方式
        /// </summary>
        public enum TradeMethod : byte
        {
            [Description("FOB")]
            FOB = 2,
            [Description("EXW")]
            EXW = 1,
            [Description("CFR")]
            CFR = 3,
            [Description("CIF")]
            CIF = 4,
            [Description("DDP")]
            DDP = 5,
            [Description("DDU")]
            DDU = 6
        }

        /// <summary>
        /// 物流方式
        /// </summary>
        public enum ShipMethod : byte
        {
            [Description("By Sea")]
            Sea = 1,
            [Description("By Air")]
            Air = 2,
            [Description("By Express")]
            Express = 3
        }

        /// <summary>
        /// 产品颜色
        /// </summary>
        public enum ProColor : byte
        {
            [Description("Blue")]
            Blue = 1,
            [Description("Green")]
            Green = 2,
            [Description("Red")]
            Red = 3,
            [Description("Gray")]
            Gray = 4,
            [Description("Navy")]
            Navy = 5,
            [Description("Orange")]
            Orange = 6
        }

        /// <summary>
        /// 获取单选框列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name">元素name</param>
        /// <param name="className">classname</param>
        /// <returns></returns>
        public static string GetRadioList(Type type, string name, string className, byte value = 0)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += string.Format("<label><input type='radio'  name='{0}' value='{1}' class='{2}' {4}/>{3}</label>", name, item.Key, className, item.Value, item.Key == value ? "checked" : "");
            }
            return result;
        }

        /// <summary>
        /// 获取单选框列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name">元素name</param>
        /// <param name="className">classname</param>
        /// <returns></returns>
        public static string GetRadioList(Type type, byte value, string name, string className)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += string.Format("<input type='radio' name='{4}' value='{0}' class='{1}' {2}/>{3} ", item.Key, className, value == item.Key ? "checked" : "", item.Value, name);
            }
            return result;
        }

        public static byte GetValue(Type type, string name)
        {
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                if (item.Value == name)
                    return item.Key;
            }
            return 0;
        }
        /// <summary>
        /// 获取多选框列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="elementName">元素的name属性值</param>
        /// <param name="className">元素Class的属性值</param>
        /// <returns></returns>
        public static string GetCheckBoxList(Type type, string elementName, string className)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += "<input type='checkbox' name='" + elementName + "' value='" + item.Key + "' class='" + className + "' />" + item.Value + " ";
            }
            return result;
        }

        /// <summary>
        /// 获取多选框列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value">选中值</param>
        /// <param name="elementName">元素的name属性值</param>
        /// <param name="className">元素Class的属性值</param>
        /// <returns></returns>
        public static string GetCheckBoxList(Type type, string value, string elementName, string className)
        {
            string result = string.Empty;
            value = "," + value.Trim(',') + ",";
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += string.Format("<input type='checkbox' name='{4}' value='{0}' class='{1}' {2}/>{3} ", item.Key, className, value.Contains("," + item.Key + ",") ? "checked" : "", item.Value, elementName);
            }
            return result;
        }

        /// 获取下拉框选项
        /// </summary>
        /// <param name="type"></param>
        public static string GetOptionHtml(Type type)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += "<option value=\"" + item.Key + "\" >" + item.Value + "</option>";
            }
            return result;
        }


        /// <summary>
        /// 获取下拉框选项
        /// </summary>
        /// <param name="type"></param>
        public static string GetOptionHtml(Type type, byte value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                result += string.Format("<option value=\"{0}\" {2}>{1}</option>", item.Key, item.Value, item.Key == value ? "selected" : "");
            }
            return result;
        }


        /// <summary>
        /// 获取下拉框选项
        /// date:2016-01-18 by Harry
        /// </summary>
        /// <param name="type"></param>
        /// <param name="values"></param>
        public static string GetOptionHtml(Type type, string values)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                if (("," + values + ",").Contains("," + item.Key + ","))
                    result += string.Format("<option value=\"{0}\">{1}</option>", item.Key, item.Value);
            }
            return result;
        }

        /// <summary>
        /// 获取下拉框选项
        /// date:2016-01-18 by Harry
        /// </summary>
        /// <param name="type"></param>
        /// <param name="values"></param>
        public static string GetOptionHtml(Type type, string values, byte value)
        {
            string result = string.Empty;
            foreach (KeyValuePair<byte, string> item in Tools.Enums.ESite.GetEnum(type))
            {
                if (("," + values + ",").Contains("," + item.Key + ","))
                    result += string.Format("<option value=\"{0}\" {2}>{1}</option>", item.Key, item.Value, item.Key == value ? "selected" : "");
            }
            return result;
        }

        /// <summary>
        /// 获取所有枚举名称 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string[] GetEnumNames(Type enumType)
        {
            return System.Enum.GetNames(enumType);
        }

        /// <summary>
        /// 获取每句的描述，获取相关值,用于页面,上绑定,要继承byte
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<KeyValuePair<byte, string>> GetEnum(Type enumType)
        {
            var names = System.Enum.GetNames(enumType);
            if (names != null && names.Length > 0)
            {
                List<KeyValuePair<byte, string>> kvList = new List<KeyValuePair<byte, string>>(names.Length);
                foreach (var item in names)
                {
                    System.Reflection.FieldInfo finfo = enumType.GetField(item);
                    object[] enumAttr = finfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true).ToArray();
                    if (enumAttr != null && enumAttr.Length > 0)
                    {
                        string description = string.Empty;

                        System.ComponentModel.DescriptionAttribute desc = enumAttr[0] as System.ComponentModel.DescriptionAttribute;
                        if (desc != null)
                        {
                            description = desc.Description;
                        }
                        kvList.Add(new KeyValuePair<byte, string>((byte)finfo.GetValue(null), description));
                    }

                }
                return kvList;

            }
            return null;
        }
        /// <summary>
        /// 获取每句的描述，获取相关值,用于页面,上绑定,要继承byte,参数indexNum获取枚举的条件
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="indexNum"></param>
        /// <returns></returns>
        public static List<KeyValuePair<byte, string>> GetEnum(Type enumType, int indexFirst, int indexLast)
        {
            var names = System.Enum.GetNames(enumType);
            if (names != null && names.Length > 0)
            {
                List<KeyValuePair<byte, string>> kvList = new List<KeyValuePair<byte, string>>(names.Length);
                foreach (var item in names)
                {
                    System.Reflection.FieldInfo finfo = enumType.GetField(item);
                    if ((byte)finfo.GetValue(null) > indexFirst && (byte)finfo.GetValue(null) < indexLast)
                    {
                        object[] enumAttr = finfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true).ToArray();
                        if (enumAttr != null && enumAttr.Length > 0)
                        {
                            string description = string.Empty;

                            System.ComponentModel.DescriptionAttribute desc = enumAttr[0] as System.ComponentModel.DescriptionAttribute;
                            if (desc != null)
                            {
                                description = desc.Description;
                            }
                            kvList.Add(new KeyValuePair<byte, string>((byte)finfo.GetValue(null), description));
                        }
                    }
                }
                return kvList;
            }
            return null;
        }

        /// <summary>
        /// 获取枚举的描述 Description
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Type enumType, object val)
        {

            string enumvalue = System.Enum.GetName(enumType, val);
            if (string.IsNullOrEmpty(enumvalue))
            {
                return "";
            }
            System.Reflection.FieldInfo finfo = enumType.GetField(enumvalue);
            object[] enumAttr = finfo.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true).ToArray();
            if (enumAttr.Length > 0)
            {
                System.ComponentModel.DescriptionAttribute desc = enumAttr[0] as System.ComponentModel.DescriptionAttribute;
                if (desc != null)
                {
                    return desc.Description;
                }
            }
            return enumvalue;
        }
    }
}
