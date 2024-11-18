## Project Overview
Endless Fighter is an action-packed 2D game built with Unity, featuring engaging combat mechanics, dynamic gameplay, and a rich RPG system. The game involves battling against enemies, managing health and energy, collecting various items, and unlocking achievements. Players can upgrade their skills, equip items, and progress through increasingly challenging levels. The game integrates combat animations, skill systems, dynamic item drops, and detailed player attributes to create an immersive experience.

## Core Classes

### 1. **DropController**
- **Purpose**: Handles the generation and dropping of collectible items (gold, experience, diamonds) when an enemy is defeated.
- **Key Features**:
  - Drops coins, experience points, and diamonds based on the level of the defeated enemy.
  - The amount of each item is calculated and divided into categories (big, medium, small), with each having a different visual size.
  - Items are instantiated and animated to "drop" in the game world.
  - Supports scalability, dropping different amounts based on the enemy's level.

### 2. **FightAnimationController**
- **Purpose**: Controls the animation states of the unit during combat, including attacks, damage reception, healing, and special abilities.
- **Key Features**:
  - Triggers various combat animations, including attack, heal, fireball, dodge, and damage received.
  - Provides visual feedback like blood effects when the unit takes damage.
  - Plays specific animations depending on the type of action, such as critical hits or dodges.
  - Tracks animation durations to synchronize actions with gameplay mechanics.

### 3. **HealthManager**
- **Purpose**: Manages the health and damage systems for units (player and enemy).
- **Key Features**:
  - Tracks current health and updates the UI accordingly (health bar, health text).
  - Handles damage calculation based on unit defense and dodge chance.
  - Supports critical hits and dodge mechanics to add variety to combat.
  - Allows for healing and health regeneration, updating the UI when health changes.
  - Instantiates damage text objects for visual feedback on the damage dealt, including color-coded text for different damage types (physical, magic, range, heal).

### 4. **Unit**
- **Purpose**: Represents a unit in the game, such as the player or enemy.
- **Key Features**:
  - Holds basic stats like health, damage, defense, and dodge chance.
  - Calculates and applies damage when receiving attacks.
  - Supports different types of damage (physical, magic, ranged) and defenses against them.
  - Level-based progression that influences stats such as health, damage, and defense.

### 5. **CollectibleMovement**
- **Purpose**: Controls the movement and collection behavior of collectible items (gold, experience, diamonds).
- **Key Features**:
  - Collectibles move towards the player after being dropped by enemies.
  - Implements random initial velocities for collectible items.
  - Once the collectible reaches the player, it updates the progress of the level (e.g., adds gold, experience, or diamonds).
  - Includes basic collision handling to remove the collectible when it reaches the target.

### 6. **AnimationEndEvent**
- **Purpose**: Provides a callback event when an animation state ends.
- **Key Features**:
  - Allows for executing specific actions once an animation is finished (e.g., triggering the next combat action or event).

### 7. **ListWithEnemyLevels**
- **Purpose**: Stores and manages enemy data, including levels and difficulty settings.
- **Key Features**:
  - Saves enemy data to PlayerPrefs to track progress across levels.
  - Includes time constraints for completing levels and difficulty adjustments based on previous player performance.

### 8. **EnergySystem**
- **Purpose**: Handles the energy system for the game, providing mechanics for regenerating energy over time.
- **Key Features**:
  - Energy regenerates over time, with configurable rates for energy per hour and regeneration intervals.
  - Integrates with UI elements (e.g., energy slider) to visually represent the player's available energy.
  - Supports saving energy data to PlayerPrefs for persistence across game sessions.

### 9. **DataInitializer**
- **Purpose**: Initializes and saves all game data (player, game progress, achievements, energy).
- **Key Features**:
  - Loads and saves player data, game data, energy data, and achievement progress.
  - Ensures data is initialized when the game starts and saved when the game ends or the application quits.

### 10. **GameData**
- **Purpose**: Manages the game data, such as player energy, current progress, and saved states.
- **Key Features**:
  - Loads and saves game-specific data to PlayerPrefs, including energy levels and last update times.
  - Tracks energy regeneration and updates the player’s available energy over time.
  - Ensures persistent storage of energy data across multiple game sessions.

### 11. **SaveLevelProgress**
- **Purpose**: Handles level completion, tracking the player's performance (such as time spent, gold, experience, and diamonds collected), and storing that data for persistence using PlayerPrefs.
- **Key Features**:
  - Saves the player's progress (stars, time, experience, gold, and diamonds).
  - Updates the UI with the results of the level (win/lose panel).
  - Saves and calculates stars based on completion time.
  - Integrates with the player's data system (PlayerData) and achievement system (FightAchievementUpdate).

### 12. **AudioManager**
- **Purpose**: Responsible for handling audio in the game, such as background music and sound effects.
- **Key Features**:
  - Plays background music and sound effects based on the scene.
  - Mutes/unmutes audio and adjusts volume.
  - Ensures that audio persists across scenes using `DontDestroyOnLoad`.

### 13. **ActiveSkill**
- **Purpose**: Handles the logic for active skills, including buffs that can be applied to a unit, and manages the timer for how long the skill lasts.
- **Key Features**:
  - Manages skill duration and buffs applied to units.
  - Updates the UI to show the remaining time of the skill.
  - Provides logic to start, update, and end a skill's effect.

### 14. **ActiveSkillController**
- **Purpose**: Manages the activation of active skills in the game’s UI.
- **Key Features**:
  - Handles skill activation by looking up skills in the `activeSkillList` and triggering the appropriate skill action.
  - Displays the skill's UI element when activated.
  - Supports the use of multiple active skills by managing them dynamically.

### 15. **MagicAttack**
- **Purpose**: Manages magic projectiles in combat, such as Fireball or Lightning Strike.
- **Key Features**:
  - Moves the magic projectile towards the target and rotates it as necessary.
  - Applies damage when the projectile reaches the target.
  - Identifies whether the projectile is cast by the player or an enemy and determines the target accordingly.

### 16. **Skill**
- **Purpose**: Represents a skill within the game, including its name, description, cooldown, and level.
- **Key Features**:
  - Defines the basic properties of a skill (name, description, cooldown, level).
  - Provides access to skill UI components like images and buttons.
  - Manages bonus effects that scale with the skill's level.

### 17. **SkillManager**
- **Purpose**: Manages all player skills, handling UI setup, skill activation, and cooldown logic.
- **Key Features**:
  - Initializes the skill UI based on available skills.
  - Manages skill cooldowns and disables buttons during cooldown periods.
  - Handles the triggering of skill effects during combat and ensures they are applied correctly.

### 18. **LevelPanelController**
- **Purpose**: Controls the level selection UI and the flow of starting a new level.
- **Key Features**:
  - Displays information about the level, such as the number of enemies and their level.
  - Handles the player’s energy and starts the level when sufficient energy is available.
  - Updates UI elements like stars to reflect the player’s performance on the level.
  - Manages the transition between levels by removing old UI elements.
    
### 19. **AchievementManager**
- **Purpose**: Manages the player's achievements, tracks progress, and handles data persistence for achievements.
- **Key Features**:
  - Tracks player stats such as level, strength, agility, intelligence, defense, etc.
  - Monitors progress toward achievement goals and unlocks achievements.
  - Saves and loads achievement data using `PlayerPrefs`.
  - Updates achievement progress based on player actions and conditions.
  - Provides functionality for checking unlocked achievements and updating the achievement status.

### 20. **AchievementPanel**
- **Purpose**: Handles the visual representation of achievements in the UI, including progress tracking, animations, and rewards.
- **Key Features**:
  - Displays achievement progress and updates in real-time.
  - Triggers animations when an achievement is unlocked.
  - Provides rewards (such as experience, coins, or diamonds) when an achievement is completed.
  - Updates achievement descriptions and progress based on player performance.
  - Animates the destruction and removal of the achievement panel once completed.

### 21. **AchievementUI**
- **Purpose**: Manages the dynamic display of achievement panels in the game's UI, visualizing progress and rewards.
- **Key Features**:
  - Instantiates and manages panels dynamically for each achievement.
  - Updates progress bars in real-time based on player data.
  - Displays appropriate rewards like experience, coins, or diamonds.
  - Handles the creation of new achievement panels when new achievements are unlocked.
  - Sets up UI components for each achievement panel, including titles, descriptions, and buttons.

### 22. **FightAchievementUpdate**
- **Purpose**: This class is responsible for updating the achievement data, including gold and diamonds earned, and tracking the number of enemies defeated by the player.
- **Key Features**:
  - **UpdateAchievementData(int gold, int diamonds)**: Increases the gold and diamonds earned by the player based on the inputs.
  - **UpdateEnemiesDefeated()**: Increases the count of defeated enemies in the player's achievement data.

### 23. **MapAchievementLevelController**
- **Purpose**: Manages the tracking of the player's progress through levels and updates achievement data accordingly.
- **Key Features**:
  - **SetLastPlayedLevel(int level)**: Updates the last played level if the new level is higher than the previous one.
  - **SetLastLevelToAchievements(float time)**: Waits for a short duration and then updates the last played level in the achievement system.

### 24. **AttributesPanelController**
- **Purpose**: Manages the display and functionality of the player's attributes panel, where the player can assign attribute points.
- **Key Features**:
  - **SetupFreeAttributesPoints()**: Displays the number of free attribute points available.
  - **SetupAttributes()**: Displays the current values of the player's attributes.
  - Methods for adding attribute points to Strength, Agility, Intelligence, and Defense.

### 25. **ExclamationMarkController**
- **Purpose**: Manages the display of exclamation marks in the player's panel to notify them of available actions (e.g., free points or unlocked achievements).
- **Key Features**:
  - **CheckData()**: Checks if there are free attribute or skill points, or if any achievements are unlocked, and sets the visibility of exclamation marks accordingly.

### 26. **DraggableItem**
- **Purpose**: Allows items to be dragged and dropped within the inventory or equipment system.
- **Key Features**:
  - Implements drag-and-drop functionality (`IBeginDragHandler`, `IDragHandler`, `IEndDragHandler`).
  - Displays item info in a panel while dragging.
  - Updates the player's inventory and equipment when the drag operation ends.

### 27. **InventoryBonusStats**
- **Purpose**: Tracks and applies bonus stats from items in the player's inventory and equipment.
- **Key Features**:
  - **UpdateStats(InventoryStatsController inventoryStatsController)**: Updates bonus stats based on the equipment items.
  - **UpdateValuesAfterLoad(List<ItemDataSS> itemsInEQ)**: Updates bonus stats based on items in the equipment.
  - Methods for resetting and applying bonus stats to the player’s attributes.

### 28. **InventorySetup**
- **Purpose**: Manages the setup and display of inventory or equipment slots, as well as handling the items within them.
- **Key Features**:
  - **GetAllItemSlotsInInventory()**: Retrieves all inventory slots in the current inventory setup.
  - **SetupInventoryItems()**: Sets up the inventory items for display.
  - **SetupEQItems()**: Sets up the equipment items for display.

### 29. **InventorySlot**
- **Purpose**: Represents a slot in the inventory where items can be dropped.
- **Key Features**:
  - Manages the type of item that can be dropped in the slot (`itemEnums`, `itemType`).
  - The `OnDrop` method checks if an item is being dropped into the slot and moves it if valid.
  - The `CheckItemType` method ensures the correct type of item is dropped into the inventory slot.
  - Handles drag-and-drop interactions with items, utilizing the `DraggableItem` component for handling the visual movement and placement of items.

### 30. **InventoryStatsController**
- **Purpose**: Manages the player's inventory stats and calculates bonus stats based on equipped items.
- **Key Features**:
  - Tracks and updates various bonuses (e.g., damage, defense, strength) based on items in the inventory.
  - Provides methods to add, remove, and sum up bonus stats for the player.
  - The `UpdateBonusStats` method is a placeholder for logic that could add or remove bonus stats from equipped items.
  - The `SumUpBonusStats` method iterates through equipped items in inventory slots and applies their bonuses to the player’s stats.
  - Handles different types of defenses and stats like strength, agility, and intelligence.

### 31. **ItemPanel**
- **Purpose**: Displays detailed information about an item in the UI.
- **Key Features**:
  - Uses the `TextMeshProUGUI` components to display item properties like name, rarity, damage, and bonuses.
  - Has an animator to control the UI transitions (e.g., idle state after a timer expires).
  - Changes the color of the upper border based on the item's rarity.
  - Displays various bonuses that the item provides (e.g., strength, agility).
  - Supports custom formatting for displaying stats in the UI with special colors and fonts.

### 32. **Requirement**
- **Purpose**: Represents a requirement needed to unlock or upgrade a skill.
- **Key Features**:
  - Specifies the requirement type (`RequirementType`), which could be an attribute or a skill.
  - Tracks whether the requirement is unlocked (`isUnlocked`).
  - Includes base level and required level for progression in the game.

### 33. **SkillDescriptionPanelController**
- **Purpose**: Manages the UI panel that displays information about a specific skill.
- **Key Features**:
  - Updates the panel with skill information such as name, level, type, description, requirements, and upgrade cost.
  - Handles the display of skill requirements and checks if they are met.
  - Controls the upgrade button's interactivity based on whether the player has enough skill points and meets all skill requirements.
  - Provides methods for unlocking and upgrading skills, updating the UI accordingly.

### 34. **SkillInfo**
- **Purpose**: Represents the skill information for a specific skill, including its upgrade state and requirements.
- **Key Features**:
  - Stores information about a skill, including its name, level, type, and description.
  - Manages the upgrade process by checking if the player can upgrade the skill.
  - The skill’s level, requirements, and upgrade costs are displayed dynamically in the UI.

## Battle System Integration

### 1. **UnitBattle**
- **Purpose**: Handles individual unit behavior during battle, including attacking, skills, and state transitions.
- **Key Features**:
  - Manages unit actions like attacking, skill usage, and dashing.
  - Integrates with the FightAnimationController to trigger animations.
  - Handles various types of damage, critical hits, and skill effects (e.g., Fireball, Double Attack).
  - States system (Idle, Dashing, Busy) to manage unit transitions during battle.

### 2. **BattleController**
- **Purpose**: Manages the flow of combat, including player and enemy actions, battle state, and win/lose conditions.
- **Key Features**:
  - Coordinates player and enemy actions through an action queue.
  - Handles unit state transitions (Idle, Busy, Dashing).
  - Integrates with the health and damage systems to track the progress of battle.
  - Handles battle victory or defeat logic based on unit status.

## Gameplay Features

### 1. **Dynamic Combat**
- Combat actions like attacking, dodging, and taking damage are all animated and visually represented.
- Damage is calculated based on unit stats, including critical hits, dodging, and defense.
- Health bars and damage indicators are shown on screen to provide feedback on combat outcomes.

### 2. **Item Drops**
- Defeated enemies drop gold, experience, and diamonds, with amounts scaled based on the enemy's level.
- Items are visually represented and animated as they drop, adding to the excitement of battle.
- Collecting items boosts the player's resources and progression.

### 3. **Health and Damage System**
- Units have dynamic health, which can decrease when taking damage and increase when healed.
- A variety of damage types exist, with each type interacting differently with defenses.
- Health and damage values are displayed in the UI, providing players with clear feedback on their status.

### 4. **Animation Integration**
- Animations are tightly integrated with combat, providing smooth transitions between actions like attacks, healing, and receiving damage.
- Blood effects and other particle systems are used to enhance the visual impact of combat.

## Benefits
- **Engaging Combat**: Dynamic animations and visual feedback make the combat experience immersive and satisfying.
- **Item Collection**: Drops after defeating enemies create an additional layer of engagement and reward.
- **Health Management**: The health system is complex, incorporating critical hits, healing, and defense to offer varied gameplay experiences.
- **Scalable**: The system allows easy expansion, supporting more item types, abilities, and combat mechanics.

## Future Enhancements
- **New Item Types**: Add additional collectible items, such as potions or buffs, that further enhance gameplay.
- **AI Improvements**: Enhance enemy AI with more complex behaviors and abilities, making battles more challenging.
- **Multiplayer Support**: Implement multiplayer capabilities, allowing players to fight together or against each other in combat.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact
For questions or contributions, feel free to open an issue or reach out via the [GitHub repository](https://github.com/EvilSasu/EndlessFighter).
