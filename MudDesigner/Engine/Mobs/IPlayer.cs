/* IPlayer
 * Product: Mud Designer Engine
 * Copyright (c) 2012 AllocateThis! Studios. All rights reserved.
 * http://MudDesigner.Codeplex.com
 *  
 * File Description: The interface contract allows for implementing properties and methods for creating player classes.
 */
//Microsoft .NET using statements
using System.Net.Sockets;
using System.Collections.Generic;

//AllocateThis! Mud Designer using statements
using MudDesigner.Engine.States;
using MudDesigner.Engine.Environment;
using MudDesigner.Engine.Core;
using MudDesigner.Engine.Objects;
using MudDesigner.Engine.Directors;

namespace MudDesigner.Engine.Mobs
{
    /// <summary>
    /// The interface contract allows for implementing properties and methods for creating player classes.
    /// </summary>
    public interface IPlayer : IMob
    {
        /// <summary>
        /// Gets or Sets the username for this player
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Gets the current State that the player is in.
        /// </summary>
        IState CurrentState { get; }

        /// <summary>
        /// Gets the character role for this player in the world
        /// </summary>
        CharacterRoles Role { get; }

        /// <summary>
        /// Gets the players unlying network connection
        /// </summary>
        Socket Connection { get; }

        /// <summary>
        /// Gets if the player is connected to the server or not.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets or Sets the buffer used by the players network connection.
        /// </summary>
        List<byte> Buffer { get; set; }

        /// <summary>
        /// Gets the input that the player has entered via their client
        /// </summary>
        string ReceivedInput { get; }

        /// <summary>
        /// Initializes the player with a default state and provides its network connection for storage.
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="connection"></param>
        void Initialize(IState initialState, Socket connection, IServerDirector director);

        /// <summary>
        /// Receives player input through the network
        /// </summary>
        /// <returns></returns>
        string ReceiveInput();

        /// <summary>
        /// Sets the players credentials
        /// </summary>
        /// <param name="password">The password the player has provided.</param>
        void SetPlayerCredentials(string userPassword);

        /// <summary>
        /// Allows the player to change their password.
        /// </summary>
        /// <param name="oldPassword">The old password that they used</param>
        /// <param name="newPassword">The new password that they want to use</param>
        /// <returns></returns>
        bool ChangePassword(string oldPassword, string newPassword);

        bool CheckPassword(string password);

        void Connect(IState initialState);
        
        void Disconnect();

        /// <summary>
        /// Switches the players State from one, to another
        /// </summary>
        /// <param name="state">The state to switch to.</param>
        void SwitchState(IState state);
        
        void OnConnect(IState initialState);

        /// <summary>
        /// Disconnects the player from the server.
        /// </summary>
        void OnDisconnect();
        
        void OnLevel(IPlayer player);
    }
}