using System;
using System.Net.Sockets;
using MelonLoader;
using CoreOSC;
using CoreOSC.IO;
using Il2Cpp;
using UnityEngine;


namespace SR_Patstrap_support
{
    public class Class1 : MelonMod
    {
        UdpClient patStrapServer;

        string ipAddress = "127.0.0.1";

        float previousFailWalls = 0f;

        bool iteration = true;
        short iteration2 = 256;
        
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"SR_Patstrap_support initialised. Using IP {ipAddress}");
            patStrapServer = new UdpClient(ipAddress, 9001); //connecting to original PatStrap server
        }

        public override async void OnFixedUpdate()
        {
            Address pl = new Address("/avatar/parameters/pat_left");
            Address pr = new Address("/avatar/parameters/pat_right");
            Address pd = new Address("/avatar/parameters/dummy");
            float x;
            if (iteration) // "emulating" headpats for server
                x = 0f;
            else
                x = 1f;
            object[] obj = new object[] { x };
            
            var msg1 = new OscMessage(pl, obj);
            var msg2 = new OscMessage(pr, obj);
            var msg3 = new OscMessage(pd, obj);
            
            GameControlManager[] gameControlManagers = Resources.FindObjectsOfTypeAll<GameControlManager>();

            if (gameControlManagers.Length > 0)
            {
                GameControlManager game = gameControlManagers[0];
                //walls in Synth Riders are too short for new method
                if (game.TotalFailWalls - previousFailWalls > 0)
                {
                    iteration2 = 0;
                }
                previousFailWalls = game.TotalFailWalls;
            }
            //so I long them out
            if (iteration2 < 11)
            {
                //screw 3d effect, there's only one "collider" provided by Synth Riders
                await patStrapServer.SendMessageAsync(msg1);
                await patStrapServer.SendMessageAsync(msg2);
                iteration2++;
                iteration = !iteration; //here, not each loop to avoid getting same values
            }
            
            await patStrapServer.SendMessageAsync(msg3); //dummy message to trick PatStrap server that VRC is working
        }
        
        public override void OnDeinitializeMelon()
        {
            patStrapServer.Close();
        }
        
    }
}