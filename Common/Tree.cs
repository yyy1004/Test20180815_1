using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Model;

namespace Common
{
    /// <summary>
    /// 菜单节点树-泛型
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class Tree<T>
    {
        public Tree()//方法 无参 构造函数
        {
            childrenN = new List<Tree<T>>();//在内存开辟空间给childrenN
        }
        //public Tree(T data)//方法 有参 构造函数  从来没有用过
        //{
        //    this.Data = data;//给字段Data赋值
        //    childrenN = new List<Tree<T>>();//在内存开辟空间给childrenN（和无参有什么区别？）

        //}

        //private Tree<T> parent;//字段 Tree<T>类型的字段
        ///// <summary>
        ///// 父节点
        ///// </summary>
        //public Tree<T> Parent//属性
        //{
        //    get { return parent; }
        //}

        /// <summary>
        /// 节点数据
        /// </summary>
        public T Data { get; set; }//属性 T类型的属性 可读可写
        /// <summary>
        /// URL
        /// </summary>
        public string URL { get; set; }//属性
        /// <summary>
        /// 目录是否展开
        /// </summary>
        public bool Open { get; set; }//属性
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int Id { get; set; }//属性

        private List<Tree<T>> childrenN;//子节点 定义了一个泛型集合 类型为Tree<T>
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Tree<T>> ChildrenN//属性
        {
            get { return childrenN; }
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
        public void AddNode(Tree<T> node)
        {
            //不含有node 就添加
            if (!childrenN.Contains(node))//确定某元素是否在List<T>中,确定Tree<T>中是否有node
            {
              //  node.parent = this;//如果没有就这个node赋值给parent 此时是root调用的
                childrenN.Add(node);//把node加入到childrenN中
            }
        }
        ///// <summary>
        ///// 添加节点
        ///// </summary>
        ///// <param name="childrenN">节点集合</param>
        //public void AddNode(List<Tree<T>> childrenN)//没有用过。
        //{
        //    foreach (var node in childrenN)
        //    {
        //        if (!childrenN.Contains(node))
        //        {
        //        //    node.parent = this;
        //            childrenN.Add(node);
        //        }
        //    }
        //}
        /// <summary>
        /// 移除子节点
        /// </summary>
        /// <param name="node"></param>
        public void Remove(Tree<T> node)
        {
            if (childrenN.Contains(node))
                childrenN.Remove(node);
        }
        /// <summary>
        /// 清空子节点集合
        /// </summary>
        public void RemoveAll()
        {
            childrenN.Clear();
        }

        /// <summary>
        /// 将树转换为Json字符串，将root及其子节点转化为字符串。
        /// </summary>
        /// <param name="root">要处理的节点</param>
        /// <returns>菜单树的Json字符串</returns>
        public static string ToJson(Tree<T> root)
        {
            string result = "";

            result += "{\"id\":" + root.Id;//添加Id

            //  result = string.Format(@"{{""id"":{0}", root.Id); asp.net v4.6才支持
            result += ",\"text\":\"" + root.Data + "\"";//添加Data
          
            result += ",\"attributes\":{\"url\":\"" + root.URL + "\"}";//添加URL
         

            if (root.ChildrenN.Count > 0)//添加子节点 如果有子节点 则存在state 和children属性。
            {
                result += ",\"state\":\"" + (root.Open ? "open" : "closed")+"\"";
              
                result += ",\"children\":";
                result += "[";
                //循环转化
                foreach (var item in root.ChildrenN)
                {
                    result += ToJson(item);
                    result += ",";
                }
                result = result.Remove(result.Length - 1, 1);//去掉最后一个“，”
                result += "]";
            }

            result += "}";
            return result;
        }

        /// <summary>
        /// 查看下一层中是否存在某一个节点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static bool Exists(Tree<string> root, string search)
        {
            if (root.ChildrenN.Count > 0)
            {
                foreach (var item in root.ChildrenN)
                {
                    if (item.Data.Equals(search))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 根据数据库中的菜单信息，设置树形菜单的URL，1、可以设置root及root的子节点的Url，2、设置Id 3、设置icon、4、设置等级。
        /// </summary>
        /// <param name="root">要处理的节点</param>
        /// <param name="menuHashT">从数据库中得到的键值对菜单表 MenuHashTable</param>
        /// <returns></returns>
        public static void SetMenuUrl(Tree<string> root, Hashtable menuHashT)
        {
            //设置当前节点url信息 管理员和设备监测和系统设置 都不在menuHashT里。所以不能从数据库中设置管理员以及设备监测和系统设置的Url。
            if (menuHashT.ContainsKey(root.Data)) //如果menuHashT里有Data;
            {
                //如果程序中已经设置过URL就采用自定义的URL，否则采用数据库中的URL
                if (root.URL == null || root.URL == "")//程序中没有设置URL
                {//采用数据库中Url
                    root.URL = ((MenuInfo)(menuHashT[root.Data])).Url;//显示类型转换 menuHashT[]中的数据是MenuInfo的一部分、数据类型一致。
                }
                root.Id = ((MenuInfo)(menuHashT[root.Data])).Id;//数据库中的Id，还可以从数据库中设置节点的icon。
            }
            //如果没有子节点，直接返回
            if (root.ChildrenN.Count > 0)
            {
                //递归遍历（只要存在子节点，就遍历子节点）
                foreach (var item in root.ChildrenN)//如果有子节点就看传过来的node设置URL
                {
                    SetMenuUrl(item, menuHashT);
                }
            }
            //return root;//不需要返回值。
        }
    }
}
