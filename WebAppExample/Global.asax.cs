using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using Autofac;
using Autofac.Integration.Web;
using NHibernate;
using NHibernate.Context;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;

namespace WebAppExample
{
    public class Global
        : HttpApplication, IContainerProviderAccessor

    {

        private static readonly ISessionFactory Sessionfactory;
        private static IContainerProvider containerProvider;

        static Global()
        {
            string rootPath = HostingEnvironment.MapPath("~");

            XmlTextReader configReader = new XmlTextReader(File.OpenRead(string.Format("{0}/Cfg/Configuration.xml", rootPath)));
            DirectoryInfo dir = new DirectoryInfo(string.Format("{0}/Mappings", rootPath));
            NhConfigurationBuilder bld = new NhConfigurationBuilder(configReader, dir);

            bld.SetProperty("connection.connection_string", GetConnectionString(rootPath));
            bld.BuildSessionFactory();

            Sessionfactory = bld.SessionFactory;
        }

        private static string GetConnectionString(string rootPath)
        {
            string output = rootPath + "db\\";

            var str = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            return string.Format(str, output);
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.Register(n => new EnterprisePagedDAO(new SessionManager(Sessionfactory)))
                .As<INhPagedDAO>()
                .SingleInstance();

            builder.Register(n => new QueryExecutor(n.Resolve<INhPagedDAO>()))
                   .As<IQueryExecutor>()
                   .SingleInstance();

            //Func<SelectorParameters, IPagingResult>
            //builder.RegisterType<Func<SelectorParameters, IPagingResult>>().AsSelf();
                
            //builder.RegisterType<BusinessDataSource>()
            //    .AsSelf()
            //    .InstancePerHttpRequest();

            //builder.Register(n => new BusinessDataSource(n.Resolve<IQueryExecutor>().SelectFunction))
            //        .AsSelf()
            //        .InstancePerHttpRequest();

            //builder.Register<Func<SelectorParameters, IPagingResult>>(n => n.Resolve<IQueryExecutor>().SelectFunction)
            //    .As<Func<SelectorParameters, IPagingResult>>();
            
            containerProvider = new ContainerProvider(builder.Build());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            lock (Sessionfactory)
            {
                CurrentSessionContext.Bind(Sessionfactory.OpenSession());
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            lock (Sessionfactory)
            {
                var session = CurrentSessionContext.Unbind(Sessionfactory);
                if (session != null && session.IsOpen)
                    session.Close();
            }
        }

        public IContainerProvider ContainerProvider
        {
            get { return containerProvider; }
        }
    }
}