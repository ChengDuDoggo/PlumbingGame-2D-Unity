一、仿接水管小游戏（2D）基础框架介绍

PS：此仿接水管小游戏基础框架只关注和完成了游戏在程序上面的玩儿法规则和功能编写，没有任何音乐和美术资源，并非一个完整品游戏！仅仅只有一个演示实例场景，演示场景中
的简单美术资源也是本人纯鼠标乱绘制的，只为了演示功能、通关条件和限制条件

玩法规则：

场景中存在着无数六边形块，每一个六边形块的边对应一个水管对接口（开发者如果想要使用其他边形代替六边形可以自行扩展）

场景中存在四个不同种类的六边形小方块：水龙头（起点）、普通水管（路径）、空六边形块和下水道（终点）

游戏的目的是玩家需要选择移动拖拽场景中的普通水管或者空六边形块，直到将水龙头和下水道通过正确的水管路径连接起来则通关

游戏中，水龙头和下水道在游戏一开始时位置固定，无法拖拽，并且它们有且仅有一个出入水口

普通水管是游戏中的主要移动块，有一个出水口和一个进水口

空六边形块可以移动拖拽，没有任何出入水口，仅仅起迷惑作用

二、编辑新的游戏关卡

本人水平有限，此小游戏框架十分简单粗糙，仅仅是为了记录自己的工作经验，并非专业程序框架开发者，如果真的有人需要使用本项目进行开发创作，首先十分感激，此外请不要
人身攻击

首先，游戏场景一切基于UGUI的Canvas系统下，开发者只需要不断拖拽“Slot”预制体在画布中并且摆放好自己想要的位置即可

“Slot”预制体分为两部分：Slot本身和和它的子物体Hexagon，它们自身各自拥有一个对应的脚本

在对应的Slot和Hexagon脚本中在Inspector面板暴露出来对应需要设定的属性，开发者只需要在Inspector面板中设定需要的字段属性

Slot：

SlotWaterPipeOfType：Water Tap
			  	    Commen Water Pipe
				    Empty Block
				    Sewer
选择当前六边形块的类型（水龙头、普通水管、空六边形块、下水道）

InletAndOutlet：Top Right
		  	   Top Left
		  	   Bottom Right
		  	   Bottom Left
		  	   Center Right
		  	   Center Left
设定出入水口在六边形的哪儿个边（水龙头和下水道只设定一个边，普通水管需要设定两个边，空六边形块不需要设定）

IsRoadStrengthBlock：true || false
设定当前方块是否是需要放置正确水管的连接路径，水龙头和下水道必须勾选，因为它们必然是游戏通关的水管路径

Hexagon：

HexagonWaterPipeOfType：Water Tap
			  	    Commen Water Pipe
				    Empty Block
				    Sewer
选择当前六边形块的类型（水龙头、普通水管、空六边形块、下水道）

InletAndOutlet：Top Right
		  	   Top Left
		  	   Bottom Right
		  	   Bottom Left
		  	   Center Right
		  	   Center Left
设定出入水口在六边形的哪儿个边（水龙头和下水道只设定一个边，普通水管需要设定两个边，空六边形块不需要设定）

在编辑一个游戏关卡时，需要先设定Slot，Slot上的属性代表着这个方块“应该”是什么属性，意思是，如果要达成游戏通关，这个Slot应该是什么属性
例如你想要场景中的某一个六边形块上面放一个进出水口为左上和右下的普通水管以达成胜利条件，那么此六边形块的SlotWaterPipeOfType应该设定
为Commen Water Pipe，InletAndOutlet设定为Top Left和Bottom Right，IsRoadStrengthBlock设定为true

设定完成Slot之后，再设定Hexagon，Hexagon上的属性代表着这个方块“现在”是什么属性
例如，你想要场景中的第二排第二个六边形块放着一个进出水口为左中和右中的普通水管，那么就设定第二排第二个六边形块的Hexagon的HexagonWaterPipeOfType
为Commen Water Pipe，InletAndOutlet为Center Left和Center Right

编辑关卡时，只需要编辑正确路径上的Slot，也就是从水龙头到下水道的路径上的Slot即可
编辑Hexagon时，一定要至少在场景中存在一个能够对应Slot属性的Hexagon属性，不然无法通关，Hexagon在场景中随机设定

通关的原理是玩家拖拽的是Slot下的Hexagon，Slot是始终不变的，拖拽Hexagon时，当检测到交换，那么拖拽的Hexagon就会移动到被拖拽的Slot下完成一次交换
当系统检测到Slot的属性和它们的Hexagon属性一模一样的时，就代表水管连接成功了！

因为水龙头和下水道无法拖拽，所有在编辑关卡时，需要手动将水龙头和下水道的Slot和Hexagon的属性设定的一模一样

这里介绍的全是文字比较抽象，只需要上手编辑一下关卡则能很快理解

------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

A, imitation of the water pipe game (2D) basic framework

PS: The basic framework of this imitation water pipe game only focuses on and completes the playing rules and functions of the game in the program above, without any music and art resources, not a complete game! There is only one demo scenario, demo scenario
The simple art resources are also drawn by my pure mouse, only to demonstrate the function, clearance conditions and restrictions

Gameplay rules:

There are countless hexagonal blocks in the scene, and each side of the hexagonal block corresponds to a water pipe interface (developers can expand if they want to use other sides instead of hexagons).

There are four different kinds of hexagonal squares in the scene: faucets (starting point), ordinary water pipes (path), empty hexagonal blocks, and sewers (end point).

The object of the game is that the player has to choose to move and drag the ordinary water pipe or the empty hexagon block in the scene until the faucet and sewer are connected through the correct water pipe path to complete the level

In the game, faucets and sewers are fixed at the beginning of the game, cannot be dragged, and they have one and only one water outlet

The ordinary water pipe is the main moving block in the game, with one water outlet and one water inlet

The empty hexagon block can be moved and dragged, without any access to water, only to confuse the effect

2. Edit new game levels

My level is limited, this small game framework is very simple and rough, just to record their work experience, not a professional program framework developer, if someone really needs to use this project for development and creation, first of all very grateful, in addition, please do not
Personal attack

First of all, under the Canvas system based on UGUI, developers only need to drag and drop the "Slot" prefab on the canvas and place it in the position they want

The Slot prefab is divided into two parts: the Slot itself and its child Hexagon, which each have a corresponding script of their own

In the corresponding Slot and Hexagon scripts, the corresponding properties are exposed in the Inspector panel, and the developer only needs to set the required field properties in the Inspector panel

Slot:

SlotWaterPipeOfType: Water Tap
Commen Water Pipe
Empty Block
Sewer
Select the type of the current hexagon block (faucet, regular water pipe, empty hexagon block, sewer)

InletAndOutlet: Top Right
Top Left
Bottom Right
Bottom Left
Center Right
Center Left
Set which side of the hexagon the water inlet and outlet is on (faucets and sewers only set one side, ordinary water pipes need to set two sides, empty hexagon blocks do not need to set)

IsRoadStrengthBlock: true || false
Set whether the current box needs to place the correct water pipe connection path, the faucet and sewer must be checked, because they must be the water pipe path through the game

Hexagon:

HexagonWaterPipeOfType: Water Tap
Commen Water Pipe
Empty Block
Sewer
Select the type of the current hexagon block (faucet, regular water pipe, empty hexagon block, sewer)

InletAndOutlet: Top Right
Top Left
Bottom Right
Bottom Left
Center Right
Center Left
Set which side of the hexagon the water inlet and outlet is on (faucets and sewers only set one side, ordinary water pipes need to set two sides, empty hexagon blocks do not need to set)

When editing a game level, you need to first set the Slot, and the properties on the Slot represent what properties the block "should" be, meaning, in order to achieve the game level, what properties should the Slot be
For example, if you want a hexagon block in the scene to have a common water pipe with top left and bottom right water inlet and exit to achieve the victory condition, then the SlotWaterPipeOfType of this hexagon block should be set
For Commen Water Pipe, InletAndOutlet is set to Top Left and Bottom Right and IsRoadStrengthBlock is set to true

After setting the Slot, then set Hexagon, and the properties on Hexagon represent the properties of this square "now"
For example, if you want the second Hexagon block in the second row of the scene to hold a common water pipe with water inlet and outlet in the left and right middle, then set the Hexagon HexagonWaterPipeOfType of the second hexagon block in the second row
Is a Commen Water Pipe, and InletAndOutlet is Center Left and Center Right

When editing a level, you only need to edit the Slot on the correct path, that is, the Slot on the path from the faucet to the sewer
When editing Hexagon, there must be at least one Hexagon attribute that can correspond to Slot attributes in the scene, otherwise, the level cannot be cleared, and Hexagon is randomly set in the scene

The principle of clearing the level is that the player drags Hexagon under the Slot, and the Slot is always unchanged. When the Hexagon is dragged, when an exchange is detected, the dragged Hexagon will move to the dragged Slot to complete an exchange
When the system detects that the Slot properties are identical to their Hexagon properties, it means that the pipe connection is successful!

Because faucets and sewers cannot be dragged and dropped, it is necessary to manually set the Slot of faucet and sewers to the same properties as Hexagon when editing levels

All the text introduced here is more abstract, only need to start editing the level can be quickly understood
