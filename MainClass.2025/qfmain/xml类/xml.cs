using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace qfmain 
{
    public class xml
    {

        public string 转义_StrToXml(string str)
        {
            文本 textSys = new 文本();
            string xt = textSys.替换(str, "&", "&amp;");
            xt = textSys.替换(xt, "<", "&lt;");
            xt = textSys.替换(xt, ">", "&gt;");
            xt = textSys.替换(xt, "\"", "&quot;");
            xt = textSys.替换(xt, "'", "&apos;");


            return xt;
        }
        public string 转义_XmlToStr(string str)
        {
            文本 textSys = new 文本();
            string xt = textSys.替换(str, "&amp;", "&");
            xt = textSys.替换(xt, "&lt;", "<");
            xt = textSys.替换(xt, "&gt;", ">");
            xt = textSys.替换(xt, "&quot;", "\"");
            xt = textSys.替换(xt, "&apos;", "'");

            return xt;
        }













        public bool  创建xml文件(string path, string 根节点, string 节点, string 子节点, string 子节点值)
        {
            try
            {
                //实例化XDocument对象
                XDocument xdoc = new XDocument();
                //创建根节点
                XElement root = new XElement(根节点);
                //创建子节点
                XElement cls = new XElement(节点);
                //    cls.SetAttributeValue("number", ""); //添加子节点的属性，如3年级2班
                //创建子节点class的子节点学生stu1

                //  XElement stu1 = new XElement("student");
                // cls.SetAttributeValue("id", "001"); //添加子节点stu1的属性，如学号001
                cls.SetElementValue(子节点, 子节点值); //添加子节点stu1的数据，如姓名张三
                                                //  stu1.SetElementValue("gender", "男"); //添加子节点stu1的数据，如性别男
                                                // stu1.SetElementValue("age", "19"); //添加子节点stu1的数据，如年龄19
                                                //创建子节点class的子节点学生stu2
                                                //XElement stu2 = new XElement("student");
                                                //stu2.SetAttributeValue("id", "002"); //添加子节点stu2的属性，如学号002
                                                //stu2.SetElementValue("name", "李晓梅"); //添加子节点stu2的数据，如姓名李晓梅
                                                //stu2.SetElementValue("gender", "女"); //添加子节点stu2的数据，如性别女
                                                //stu2.SetElementValue("age", "18"); //添加子节点stu2的数据，如年龄18
                                                //  cls.Add(stu1); //添加student到class
                                                //cls.Add(stu2); //添加student到class
                root.Add(cls); //添加子节点class到根节点school
                xdoc.Add(root); //添加根节点到XDoucment对象
             
                xdoc.Save(path); //使用XML的保存会自动在xml文件开始添加：<?xml version="1.0" encoding="utf-8"?>

                return true;

            }
            catch (Exception)
            {
                return false;
              //  throw;
            }

        }

        public bool  修改xml文件(string path, string 节点, string 子节点, string 子节点值)
        {
            try
            {
                XDocument xdoc = XDocument.Load(path);
                XElement xeleRoot = xdoc.Root;

                //   XElement xeleClass = xeleRoot.Elements("class").Where(x => x.Attribute("number").Value == "0302").Single(); //获取班级号为0302的直接子节点

                XElement xeleClass = xeleRoot.Elements(节点).Single();

                //  XElement xeleStudent = xeleClass.Elements("student").Where(x => x.Attribute("id").Value == "001").Single(); //获取学号为001的直接子节点的下一级节点
                //  xeleStudent.SetAttributeValue("id", "008");
                // xeleStudent.SetElementValue("name", "邦德");
                xeleClass.SetElementValue(子节点, 子节点值);
                //xeleStudent.SetElementValue("gender", "爷们");
                //xeleStudent.SetElementValue("age", "39");
                xdoc.Save(path);
                //Console.WriteLine("修改成功！");
                //Console.ReadKey();
                return true;
            }
            catch (Exception ex)
            {
                
               // MessageBox.Show(ex.Message);
                return false;
                ;                //throw;
            }
        }

        public bool  删除根节点的直接子节点(string path ,string 节点)
        {
            try
            {

               
                    XDocument xdoc = XDocument.Load(path);
                    XElement xeleRoot = xdoc.Root;

                    //删除根节点的直接子节点
                  //  XElement xeleClass = xeleRoot.Elements("class").Where(x => x.Attribute("number").Value == "0302").Single(); //拉姆达表达式

                XElement xeleClass = xeleRoot.Elements(节点).Single();

                xeleClass.Remove();

                    //删除根节点的直接子节点的下一级节点
                    //XElement xeleClass = xeleRoot.Elements("class").Where(x => x.Attribute("number").Value == "0302").Single(); //获取班级号为0302的直接子节点
                    //XElement xeleStudent = xeleClass.Elements("student").Where(x => x.Attribute("id").Value == "001").Single(); //获取学号为001的直接子节点的下一级节点
                    //xeleStudent.Remove();

                    xdoc.Save(path);
                  //  Console.WriteLine("删除节点成功！");
                  // Console.ReadKey();
        

         
                return true;
            }
            catch (Exception)
            {
                return false;
              //  throw;
            }
        }


        public bool 删除根节点的子节点(string path, string 节点,string 子节点)
        {
            try
            {


                XDocument xdoc = XDocument.Load(path);
                XElement xeleRoot = xdoc.Root;

                //删除根节点的直接子节点
                //  XElement xeleClass = xeleRoot.Elements("class").Where(x => x.Attribute("number").Value == "0302").Single(); //拉姆达表达式

                //XElement xeleClass = xeleRoot.Elements(节点).Single();

                //xeleClass.Remove();

                //删除根节点的直接子节点的下一级节点
                XElement xeleClass = xeleRoot.Elements(节点).Single(); //获取班级号为0302的直接子节点
                XElement xeleStudent = xeleClass.Elements(子节点).Single(); //获取学号为001的直接子节点的下一级节点
             xeleStudent.Remove();

                xdoc.Save(path);
                //  Console.WriteLine("删除节点成功！");
                // Console.ReadKey();



                return true;
            }
            catch (Exception)
            {
                return false;
                //  throw;
            }
        }

        public  void 添加节点(string path,string 节点 ,string 子节点,string 子节点值)
        {
           // string path = "1.xml";
            XDocument xdoc = XDocument.Load(path);
            XElement xeleRoot = xdoc.Root;

      

            //在根节点下添加新的直接子节点及次级节点的属性和数据
         XElement xeleClass = new XElement(节点);

            //xeleClass.SetAttributeValue("number", "0501");
            //XElement xeleStu1 = new XElement("student");
            //xeleStu1.SetAttributeValue("id", "003");

            //  XElement xeleStu1 = xeleRoot.Elements(节点).Single();         
             xeleClass.SetElementValue(子节点 ,子节点值);

            //xeleStu1.SetElementValue("gender","女");
            //xeleStu1.SetElementValue("age","26");
            //XElement xeleStu2 = new XElement("student");
            //xeleStu2.SetAttributeValue("id", "004");
            //xeleStu2.SetElementValue("name", "王亮");
            //xeleStu2.SetElementValue("gender", "男");
            //xeleStu2.SetElementValue("age", "36");
            //xeleClass.Add(xeleStu1);
            //xeleClass.Add(xeleStu2);
           xeleRoot.Add(xeleClass);

            xdoc.Save(path);
            //Console.WriteLine("添加xml成功");
            //Console.ReadKey();
        }

        public bool  添加子节点_子节点存在为修改不存在为添加(string path, string 节点, string 子节点, string 子节点值)
        {
            try
            {
                // string path = "1.xml";
                XDocument xdoc = XDocument.Load(path);
                XElement xeleRoot = xdoc.Root;

                //在已存在的节点上添加属性和数据
                //  XElement xeleClass = xeleRoot.Element(节点);
                //  XElement xeleStu3 = new XElement(子节点);
                //  xeleStu3.SetAttributeValue(子节点, 节点值);
                //xeleStu3.SetElementValue(子节点, 节点值);
                //XElement xeleStu3 = xeleRoot.Elements(节点).Single();
                //xeleStu3.SetElementValue(子节点, 节点值);

                //xeleStu3.SetElementValue("gender", "男");
                //xeleStu3.SetElementValue("age", "40");
                //   xeleClass.Add(xeleStu3);
                // xeleStu3.Add(xeleStu3);


                //在根节点下添加新的直接子节点及次级节点的属性和数据
                XElement xeleClass = new XElement(节点);
                //xeleClass.SetAttributeValue("number", "0501");
                XElement xeleStu1 = xeleRoot.Elements(节点).Single();
                //xeleStu1.SetAttributeValue("id", "003");
                xeleStu1.SetElementValue(子节点, 子节点值);

                //xeleStu1.SetElementValue("gender","女");
                //xeleStu1.SetElementValue("age","26");
                //XElement xeleStu2 = new XElement("student");
                //xeleStu2.SetAttributeValue("id", "004");
                //xeleStu2.SetElementValue("name", "王亮");
                //xeleStu2.SetElementValue("gender", "男");
                //xeleStu2.SetElementValue("age", "36");
                xeleClass.Add(xeleStu1);
                //xeleClass.Add(xeleStu2);
                //xeleRoot.Add(xeleClass);

                xdoc.Save(path);
                //Console.WriteLine("添加xml成功");
                //Console.ReadKey();

                return true;
            }
            catch (Exception)
            {
                return false;
             //  throw;
            }
        }

       public  bool   取_所有节点名(string path,List<string> 节点名)
        {

            try
            {
              
           //   节点名 = new List<string> { } ;

                XDocument xdoc = XDocument.Load(path); //加载xml文件
                XElement rootSchool = xdoc.Root; //获取根元素
                                                 //Console.WriteLine(rootSchool.Name); //根元素的名字
                IEnumerable<XElement> xeles = rootSchool.Elements(); //获取根元素下所有的直接子元素
                foreach (XElement xeleClass in xeles)
                {
                string jd=   xeleClass.Name.ToString();
                    //MessageBox.Show(jd);
                    //foreach (XElement xeleStudent in xeleClass.Elements())
                    //{
                    节点名.Add(jd);               

                    //    string zjd = xeleStudent.Name.ToString();

                    //    MessageBox.Show(zjd);
                    //    //    Console.WriteLine(xeleStudent.Name); //获取节点名
                    //    //    Console.WriteLine(xeleStudent.Attribute("id").Value); //获取属性值
                    //    //    Console.WriteLine(xeleStudent.Element("name").Value); //下面3行是获取数据
                    //    //    Console.WriteLine(xeleStudent.Element("gender").Value);
                    //    //    Console.WriteLine(xeleStudent.Element("age").Value);
                    //   }
                    }
                //   Console.ReadKey();
                                                                                                                                     

                return true;
            }
            catch (Exception ex)
            {
                return false;
                MessageBox .Show (ex.Message );
               // throw;
            }
        }


        public bool 取_所有子节点名(string path,string 节点, List<string> 子节点名)
        {

            try
            {

                //   节点名 = new List<string> { } ;

        XDocument xdoc = XDocument.Load(path); //加载xml文件
              //  XElement rootSchool = xdoc.Root; //获取根元素
                                                 //Console.WriteLine(rootSchool.Name); //根元素的名字
                                                 //  IEnumerable<XElement> xeles = rootSchool.Elements(); //获取根元素下所有的直接子元素
                                                 //foreach (XElement xeleClass in xeles)
                                                 //{



                XElement xeleRoot = xdoc.Root;
                XElement xeleClass =xeleRoot.Elements(节点).Single();
             


                //string jd = xeleClass.Name.ToString();
                //MessageBox.Show(jd);
                foreach (XElement xeleStudent in xeleClass.Elements())
                    {             

               string zjd = xeleStudent.Name.ToString();
                    子节点名.Add(zjd);
                    //    MessageBox.Show(zjd);
                    //    //    Console.WriteLine(xeleStudent.Name); //获取节点名
                    //    //    Console.WriteLine(xeleStudent.Attribute("id").Value); //获取属性值
                    //    //    Console.WriteLine(xeleStudent.Element("name").Value); //下面3行是获取数据
                    //    //    Console.WriteLine(xeleStudent.Element("gender").Value);
                    //    //    Console.WriteLine(xeleStudent.Element("age").Value);
                }
              
                //   Console.ReadKey();


                return true;
            }
            catch (Exception ex)
            {
                return false;
               // MessageBox.Show(ex.Message);
                // throw;
            }
        }

        public bool 取_所有子节点值(string path, string 节点,string 子节点, ref string 子节点值)
        {

            try
            {
                                                          

                //   节点名 = new List<string> { } ;

                XDocument xdoc = XDocument.Load(path); //加载xml文件
                                                       //  XElement rootSchool = xdoc.Root; //获取根元素
                                                       //Console.WriteLine(rootSchool.Name); //根元素的名字
                                                       //  IEnumerable<XElement> xeles = rootSchool.Elements(); //获取根元素下所有的直接子元素
                                                       //foreach (XElement xeleClass in xeles)
                                                       //{



                XElement xeleRoot = xdoc.Root;
                XElement xeleClass = xeleRoot.Elements(节点).Single();

                string zjdz = xeleClass.Element(子节点).Value;
                子节点值=zjdz;

                //string jd = xeleClass.Name.ToString();
                ////MessageBox.Show(jd);
                //foreach (XElement xeleStudent in xeleClass.Elements())
                //{

               
                    //    MessageBox.Show(zjd);
                    //    //    Console.WriteLine(xeleStudent.Name); //获取节点名
                    //    //    Console.WriteLine(xeleStudent.Attribute("id").Value); //获取属性值
                    //    //    Console.WriteLine(xeleStudent.Element("name").Value); //下面3行是获取数据
                    //    //    Console.WriteLine(xeleStudent.Element("gender").Value);
                    //    //    Console.WriteLine(xeleStudent.Element("age").Value);
            //    }

                //   Console.ReadKey();


















                return true;
            }
            catch (Exception ex)
            {
                return false;
                MessageBox.Show(ex.Message);
                // throw;
            }
        }



    }

}
