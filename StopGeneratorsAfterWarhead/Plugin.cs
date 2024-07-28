using System;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs.Warhead;
using WarheadHandler = Exiled.Events.Handlers.Warhead;

namespace StopGeneratorsAfterWarhead
{
	public class Plugin : Plugin<Config>
	{
		private EventHandler EventHandler;
		public static Plugin Singleton;

		public override string Name { get; } = "StopGeneratorsAfterWarhead";
		public override string Author { get; } = "Denty";
		public override Version Version { get; } = new Version(1, 0, 0);
		public override Version RequiredExiledVersion { get; } = new Version(8, 0, 0);


		public override void OnEnabled()
		{
			Singleton = this;

			EventHandler = new EventHandler();

			WarheadHandler.Detonating += EventHandler.DetonatingEvent;

			base.OnEnabled();
		}


		public override void OnDisabled()
		{
			WarheadHandler.Detonating -= EventHandler.DetonatingEvent;
		
			EventHandler = null;
			Singleton = null;

			base.OnDisabled();
		}
	}


	public class EventHandler
	{
		public void DetonatingEvent(DetonatingEventArgs ev)
		{
			if (ev.IsAllowed)
			{
				foreach (Generator state in Generator.List)
					state.State = Exiled.API.Enums.GeneratorState.None;
			}
		}
	}


	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;
		public bool Debug { get; set; } = false;
	}
}