using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudDesigner.MudEngine.GameObjects.Environment;

namespace MudDesigner
{
    /// <summary>
    /// Toolkit settings, not related to the MUD engine or project.
    /// Saved using the Engines Filesystem.
    /// </summary>
    public sealed class Settings
    {
        /// <summary>
        /// Various development stages that the kit can be in.
        /// </summary>
        public enum KitStages
        {
            Preview_Release,
            Development_Source_Only,
            Alpha,
            Beta,
            Final,
        }
#warning Reminder: Change Settings.VersionStage to KitStages.Preview upon release.

        public KitStages VersionStage = KitStages.Development_Source_Only;

        public Realm DefaultRealm;

        public string GetVersion()
        {
            string stage = VersionStage.ToString().Replace("_", " ");
            Version version = new Version(Application.ProductVersion);
            string versionID = version.Major + "." + version.Minor + "." + version.Revision + ":" + version.Build;

            return stage + " " + versionID;
        }
    }
}
