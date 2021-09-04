using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Enums
{
    public enum ModerationType
    {
        [Description("Political Propoganda")]
        Political,
        [Description("Offensive Language")]
        Language,
        [Description("Drug or Alcohol references")]
        ProhibitedSubstances,
        [Description("Hate Speech")]
        HateSpeech,
        [Description("Sexual Content")]
        Sexual,
        [Description("Targeted Shaming")]
        Shaming,
        [Description("Threatening Speech")]
        Threatening
    }
}
