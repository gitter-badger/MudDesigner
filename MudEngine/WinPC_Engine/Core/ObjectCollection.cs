using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MudEngine.Game;
using MudEngine.GameScripts;
using MudEngine.Scripting;

namespace MudEngine.Core
{
    internal class ObjectCollection : ICollection<BaseScript>
    {
        #region Public Properties
        public Boolean IsReadOnly
        {
            get { return this.isReadOnly; } //TODO: Rename to ReadOnly and _ReadOnly
        }
        #endregion

        #region public Methods

        #endregion

        #region public Interface Methods
        /// <summary>
        /// Adds a new Scripted Object to the games Object Manager.
        /// </summary>
        /// <param name="item"></param>
        public void Add(BaseScript item)
        {
            //Checks if the Object Manager is currently in Read-Only mode.
            if (this.isReadOnly)
            {
                Logger.WriteLine("Warning: Attempted to add a scripted object while the Object Manager is Read Only.");
                return;
            }

            //TODO: Allow the same Scripts to be used provided different ID's are assigned.
            if (!this.Contains(item))
                this.scriptCollection.Add(item);
            else
                Logger.WriteLine("Warning: Scripted object (" + item.ID.ToString() + ")" + item.Name + " was not added to the Object Manager due to being a duplicate.");
        }

        /// <summary>
        /// Clears the Object Manager of all Scripted Objects
        /// </summary>
        public void Clear()
        {
            this.scriptCollection.Clear();
        }

        /// <summary>
        /// Checks if the Object Manager currently has a Scripted Object matching the supplied Object.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Boolean Contains(BaseScript item)
        {
            bool isFound = false;

            foreach (BaseScript script in this.scriptCollection)
            {
                if (script.Equals(item))
                {
                    isFound = true;
                    break;
                }
            }

            return isFound;
        }

        public void CopyTo(BaseScript[] array, int arrayIndex)
        {
            this.scriptCollection.CopyTo(array, arrayIndex);
        }

        public Int32 Count
        {
            get { return this.scriptCollection.Count; }
        }

        public bool Remove(BaseScript item)
        {
            if (this.Contains(item))
            {
                this.scriptCollection.Remove(item);
                return true;
            }

            return false;
        }

        public Int32 Length { get { return this.scriptCollection.Count; } }

        public IEnumerator<BaseScript> GetEnumerator()
        {
            return new ScriptEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ScriptEnumerator(this);
        }
        #endregion

        #region Constructors
        public ObjectCollection(StandardGame game)
        {
            this.scriptCollection = new List<BaseScript>();
            this.isReadOnly = false;

            this._Game = game;
        }

        /// <summary>
        /// Get or Set a index value for this collection
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseScript this[Int32 index]
        {
            get 
            {
                if (index <= this.Count - 1)
                    return (BaseScript)this.scriptCollection[index];
                else //If the index is out of bounds, just return the last item in the collection.
                    return (BaseScript)this.scriptCollection[this.scriptCollection.Count - 1];
            }
            set 
            { 
                if (!this.IsReadOnly)
                    this.scriptCollection[index] = value; 
            }
        }
        #endregion

        #region Private Fields
        private List<BaseScript> scriptCollection;
        private Boolean isReadOnly;
        private StandardGame _Game;
        #endregion
    }
}
