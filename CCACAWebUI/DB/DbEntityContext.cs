using CCACAWebUI.Models;
using Microsoft.EntityFrameworkCore;

namespace CCACAWebUI.DB
{
    public class DbEntityContext : DbContext
    {
        public static string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<T_User> Users { get; set; }

        /// <summary>
        /// 配置表
        /// </summary>
        public DbSet<T_Configure> Configures { get; set; }

        /// <summary>
        /// 信息表
        /// </summary>
        public DbSet<T_Information> Informations { get; set; }

        /// <summary>
        /// 语言表
        /// </summary>
        public DbSet<T_Language> Languages { get; set; }

        /// <summary>
        /// 首页轮播图表
        /// </summary>
        public DbSet<T_Carousel> Carousels { get; set; }

        /// <summary>
        /// 会员链接
        /// </summary>
        public DbSet<T_MemberLink> MemberLinks { get; set; }

        /// <summary>
        /// 产品信息
        /// </summary>
        public DbSet<T_ProjectInfo> ProjectInfos { get; set; }

        /// <summary>
        /// 关联表(翻译语言)
        /// </summary>
        public DbSet<T_Ref> Refs { get; set; }

        /// <summary>
        /// 类型表
        /// </summary>
        public DbSet<T_Type> Types { get; set; }

        /// <summary>
        /// 字典表
        /// </summary>
        public DbSet<T_WordDict> WordDicts { get; set; }

        /// <summary>
        /// 首页图片
        /// </summary>
        public DbSet<T_Home> Home { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public DbSet<T_File> File { get; set; }

        /// <summary>
        /// 会员特权
        /// </summary>
        public DbSet<T_Member> Member { get; set; }

        public DbSet<T_NewActive> NewActive { get; set; }

        /// <summary>
        /// 协作单位
        /// </summary>
        public DbSet<T_Company> Company { get; set; }
    }
}
