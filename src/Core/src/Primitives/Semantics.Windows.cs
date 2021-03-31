﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Maui
{
	public enum SemanticHeadingLevel
	{
		Default = -1,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Level5 = 5,
		Level6 = 6,
		Level7 = 7,
		Level8 = 8,
		Level9 = 9,
		None = 0
	}

	public partial class Semantics
	{
		public SemanticHeadingLevel HeadingLevel { get; set; } = SemanticHeadingLevel.Default;
	}
}
