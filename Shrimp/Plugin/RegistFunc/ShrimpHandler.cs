using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shrimp.Plugin.RegistFunc
{
	public class ShrimpHandler
	{
		public delegate void testHandler ();
		public event testHandler testHandlerEvent;

		public void DoHandler()
		{
			if (testHandlerEvent != null)
				testHandlerEvent.Invoke();
		}
	}
}
