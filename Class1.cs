using System;
using System.Net.Sockets;
using MelonLoader;
using CoreOSC;
using CoreOSC.IO;
using UnityEngine;


namespace SR_Patstrap_support
{
    public class Class1 : MelonMod
    {
        UdpClient patStrapServer;

        string ipAddress = "127.0.0.1";

        float previousFailWalls = 0f;
        
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"SR_Patstrap_support initialised. Using IP {ipAddress}");
            patStrapServer = new UdpClient(ipAddress, 9001); //connecting to original PatStrap server
        }

        public override async void OnFixedUpdate()
        {
            System.Random random = new System.Random();
            Address pl = new Address("/avatar/parameters/pat_left");
            Address pr = new Address("/avatar/parameters/pat_right");
            Address pd = new Address("/avatar/parameters/dummy");
            float x = (float) random.NextDouble() * 0.5F + 0.25F; //required, to simulate pat for PatStrap server so it will activate motors
            object[] obj = new object[] { x };
            
            var msg1 = new OscMessage(pl, obj);
            var msg2 = new OscMessage(pr, obj);
            var msg3 = new OscMessage(pd, obj);

            GameControlManager[] gameControlManagers = Resources.FindObjectsOfTypeAll<GameControlManager>();

            if (gameControlManagers.Length > 0)
            {
                GameControlManager game = gameControlManagers[0];
                if (game.TotalFailWalls - previousFailWalls > 0)
                {
                    //screw 3d effect, there's only one "collider" provided by Synth Riders
                    await patStrapServer.SendMessageAsync(msg1);
                    await patStrapServer.SendMessageAsync(msg2);
                }
                //MelonLogger.Msg(game.TotalFailWalls); //for debug purposes
                previousFailWalls = game.TotalFailWalls;
            }
            
            await patStrapServer.SendMessageAsync(msg3); //dummy message to trick PatStrap server that VRC is working
        }
        
        public override void OnDeinitializeMelon()
        {
            patStrapServer.Close();
        }
        
    }
}