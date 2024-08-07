# SR_Patstrap_support
A free Melon mod for Synth Riders which adds [Patstrap](https://github.com/danielfvm/Patstrap) support to the game.

How it works? It makes your Patstrap vibrate when you walk into the wall, that's it!

## Setup

### PatStrap side
I assume that you already built hardware and setup software of PatStrap itself. If not, [follow original instructions first](https://github.com/danielfvm/Patstrap?tab=readme-ov-file#hardware). You don't have to setup VRC avatar if you won't play VRC, though.

### Synth Riders side
This mod is made for PCVR version of Synth Riders (both Steam and Oculus). It won't work on Quest in standalone mode, but it will work in tethered mode *(Virtual Desktop, Steam Link, Meta Quest (Air) Link, ALVR etc.)*

Of course, your game copy has to be modded. If it isn't, [SynthRiderz wiki](https://wiki.synthriderz.com/en/guides/installing-mods) is the best place to get started.

To install it, you have to download both *.dll files and drag `SR_Patstrap_support.dll` into `%SynthRiders_directory%/Mods/` folder. The second file is `CoreOSC.dll` library, which is used by this mod, and which is distributed with it for your convenience. You should drag it into `%SynthRiders_directory%/UserLibs/` folder.

When you bootup the game, `VRChat connection` in PatStrap server should turn blue. If it is, congrats.

If not, and you did everything correctly, but VRC works and it's also setup correctly, feel free to open an issue.

## Demonstration

TODO

## Credits

[PatStrap](https://github.com/danielfvm/Patstrap) for the original project. This mod wouldn't exist without it.

[CoreOSC](https://github.com/dastevens/CoreOSC) for the C# library to implement OSC protocol. Licensed under MIT License.

## You would also like

[BS_Patstrap_support](https://github.com/tapafon/BS_Patstrap_support) same mod, but for Beat Saber