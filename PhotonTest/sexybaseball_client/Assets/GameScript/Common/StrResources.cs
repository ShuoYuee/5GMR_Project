using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrResources
{
    namespace AssetBundle
    {
        public struct Logo
        {
            public const string bundleName = "logo";
            /*
            /// <param name="teamID">Team ID (Start at 1 not 0.)</param>
            /// <returns>File name.</returns>
            public static string GetTeamLogoFileName(int teamID)
            {
                return "team" + teamID.ToString();
            }

            /// <param name="girlID">Girl ID (Start at 1 not 0.)</param>
            /// <returns>File name.</returns>
            public static string GetStoryGirlLogoFileName(int girlID)
            {
                return "girl1" + girlID.ToString();
            }

            /// <param name="girlID">Girl ID (Start at 1 not 0.)</param>
            /// <returns>File name.</returns>
            public static string GetChallengeGirlLogoFileName(int girlID)
            {
                return "girl2" + girlID.ToString();
            }*/
        }

        public struct GirlsTachies
        {
            /// <param name="girlID">Girl ID (Start at 1 not 0.)</param>
            /// <returns>Asset bundle name.</returns>
            public static string GetBundleName(int girlID)
            {
                return "girl" + girlID.ToString() + "tachies";
            }
        }
    }
}
