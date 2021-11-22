# LSW-Interview-ProjectLSW INTERVIEW DOCUMENTATION
By
Chukwunonso “Sparda” Joseph

CLASSES
Managers
•	CameraController 
 Is attached to cameraRig gameobject and updates the camera to the player position.

•	FactoryManager 
 Singleton object where all the factories created within the game is assigned and can be referenced from.

•	InputManager 
Handles all player inputs.

•	InventoryUI 
Abstract class where all inventory UI manager classes derive from.

•	PlayerInventoryUiManager
Manages all the UI objects for the player inventory. It derives form inventoryui class.

•	ShopInventoryUimanager
Manages all UI objects for the shop Inventory.

•	NpcManager 
 Handles and keeps track of all NPC objects in the game.

•	QuestManger 
 Handles the creation and assignment of quests to the NPC, so that the player can get them after interacting with an NPC.

•	UIManager
 Handles all the Ui within the game.


Components
•	NPCObject 
 The default component for all NPC objects.

•	InteractionsController 
 Controls all the player interaction with the world.

•	MovementController 
 Handles all player movement within the Game.

•	Player 
 Handles all core player functionality.

•	UiInventoryitem 
Handles the display and updating a single uiItem when its variable values changes.

•	Quest 
Is a class the holds all the data about a quest.

•	Item 
Defines an instance of an item in the world.

•	Shop 
Handles all core shop functionality.

ScriptableObjects
•	ItemFactory 
 Stores all the properties of items within the game.

•	Inventory 
Stores all the item and amount for a particular object.

•	ItemProps 
Stores all the properties of an item type.



Other
•	EnumBase 
NameSpace that handles all the enums created and used within the game.


APPROACH
The major System in this presentation is the inventory system. I wanted to implement an easily reusable Inventory that any object within the game can have and store items within.
So, I opted for using scriptable objects for the Inventory system. I also included a quest system so that the player can have a means to make money to be spent in the shop.

CONCLUSION
The task hasn’t been easy (like everything good that comes with game dev), but this is a pretty good game and system for something that was implemented within a short period of time.
All code and character sprites were all created and implemented from scratch for this project.
