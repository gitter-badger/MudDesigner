using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MudDesigner.Engine.Core;
using MudDesigner.Engine.Properties;
using MudDesigner.Engine.Scripting;

namespace MudDesigner.Editor
{
    public partial class frmEngineSettings : Form
    {
        public frmEngineSettings()
        {
            InitializeComponent();
        }

        private void frmEngineSettings_Load(object sender, EventArgs e)
        {
            //Get all of the Game Scripts
            Type[] objects = ScriptFactory.GetTypesWithInterface("IGame");

            if (objects.Length == 0)
                MessageBox.Show("Warning! You do not have any scripts that inherit from MudDesigner.Engine.Core.Game!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            foreach (Type game in objects)
            {
                defaultGameType.Items.Add(game.Name);
            }
            if (defaultGameType.Items.Count > 0)
                defaultGameType.SelectedIndex = 0;

            //Player scripts
            objects = ScriptFactory.GetTypesWithInterface("IPlayer");

            if (objects.Length == 0)
                MessageBox.Show("Warning! You do not have any scripts that inherit from MudDesigner.Engine.Mobs.BasePlayer!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            foreach (Type player in objects)
            {
                defaultPlayerType.Items.Add(player.Name);
            }
            if (defaultPlayerType.Items.Count > 0)
                defaultPlayerType.SelectedIndex = 0;

            //Login scripts
            objects = ScriptFactory.GetTypesWithInterface("IState");

            if (objects.Length == 0)
                MessageBox.Show("Warning! You do not have any scripts that implement MudDesigner.Engine.States.IState!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            foreach (Type loginScript in objects)
            {
                loginSuccessState.Items.Add(loginScript.Name);
            }
            if (loginSuccessState.Items.Count > 0)
                loginSuccessState.SelectedIndex = 0;
        }
    }
}
