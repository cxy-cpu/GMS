﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace GMS
{
    public class SqlHelper
    {
        //-----------------------------------数据是否存在或者正确
        //静态类要将参数也改为静态，调用App.config
        public static readonly string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        //静态类，调用静态方法时,可以使用"类名.方法名"的方式,无需创建对象
        public static object ExecuteScalar(string sql, params MySqlParameter[] paras)
        {
            object o = null;
            //建立数据库的连接      
            //使用using，可以不用关闭连接
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                //创建Command对象(MySqlCommand用来执行数据库操作命令)
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //添加参数
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                //打开连接(最晚打开，最早关闭)
                //MessageBox.Show(connString);
                conn.Open();
                //执行命令
                o = cmd.ExecuteScalar();//执行查询，返回结果集第一行第一列的值，忽略其他行或列
                                        //关闭连接
                                        //conn.Close();
            }
            return o;
        }



        //------------------------------------------查
        public static DataTable GetDataTable(string sql, params MySqlParameter[] paras)
        {

            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                //创建Command对象(MySqlCommand用来执行数据库操作命令)
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (paras != null)
                {
                    //添加参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                //数据填充
                adapter.Fill(dt);
            }
            return dt;
        }

        //------------------------------------------------增删改
        public static int ExecuteNonQuery(string sql, params MySqlParameter[] paras)
        {
            int count = 0;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {

                //创建Command对象(MySqlCommand用来执行数据库操作命令)
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //添加参数
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                //打开连接(最晚打开，最早关闭)
                conn.Open();
                //执行命令
                count = cmd.ExecuteNonQuery();//返回受影响的行数(增加，修改，删除)
            }
            return count;
        }


        //------------------------------修改操作传值
        public static MySqlDataReader ExecuteReader(string sql, params MySqlParameter[] paras)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //添加参数
                if (paras != null)
                {
                    //添加参数
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                conn.Open();
                //此时不使用using，读取数据过程中不能关闭
                MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception("执行查询出现异常", ex);
            }
        }
    }
}
