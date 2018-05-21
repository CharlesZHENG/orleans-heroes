using System;
using Grace.DependencyInjection;
using Heroes.Contracts.Grains;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Runtime;

namespace Heroes.SiloHost.ConsoleApp.Infrastructure
{
	/// <summary>
	/// <see cref="IGrainActivator"/> that uses type activation to create grains.
	/// </summary>
	public class TenantGrainActivator : DefaultGrainActivator
	{
		public TenantGrainActivator(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public override object Create(IGrainActivationContext context)
		{
			var scope = context.ActivationServices.GetRequiredService<IExportLocatorScope>();

			var tenantContext = new AppTenant
			{

			};
			//var t = RequestContext.Get("tenant");
			//vasr teecontext.Items["tenant"];
			if (context.GrainIdentity.PrimaryKeyString != null && context.GrainIdentity.PrimaryKeyString.Contains("tenant/"))
			{
				var keySplit = context.GrainIdentity.PrimaryKeyString.Split('/');
				tenantContext.Key = keySplit[1];
			}


			scope.SetExtraData("TenantContext", tenantContext);


			return base.Create(context);
		}
	}
}