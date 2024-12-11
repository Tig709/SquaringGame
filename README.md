# Squaring

**Squaring** is an open source card game made in Unity. The game features a unique card mechanic and features can serves as a modular base for creating other card-based games.

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Scripts Overview](#scripts-overview)
- [Contributing](#contributing)
- [License](#license)

## Description

**Squaring** is a card game that challenges players to strategically turn around cards. The game features modular systems, allowing for easy customization and extension to suit various card game genres. This project can also be used as a base for developers to create other card-based games in Unity.

## Features

- **Core Game Mechanics:** Implementing a unique gameplay mechanic involving card combinations.

- **Modular scripts:** This project introducs reusable script for card games

- **Easy customizable:** Easily customizable to fit different types of cards

- **Multiplayer support:** The game is local multiplayer, users play on the same screen

## Installation

1. Clone the repository
-  git clone https://Github.com/Tig709/SquaringGame.git 

<br>

2. Open the project
- Open the project in Unity version [2022.3.8f1]

<br>

3. In Unity, go to **Window > Package Manager**
- Make sure that the TextMeshPro package is installed

<br>

4. In Unity, navigate to **Assets > Scenes**
- Open StartScene to try the game out 

## Usage

1. Start the game by opening the .exe file or in Unity, navigate to **Assets > Scenes** and open StartScene

<br>

2. Fill in the names of the players and start the game

<br>

3. Start by clicking on a card, the card should be next to an open card.
- Be carefull once you have chosen a card, you can not go back

<br>

4. Use the buttons to tell if the chosen card is higher or lower
- If a card has 2 open neighbours you have to tell if the value of the card is inbetween or outside of the other 2 cards
    - If a card has 3 open neighbours the game will choose the easiest combination to solve

<br>

5. The card will be turned around and you will see if your prediction was right

<br>

6. You have to get 2 cards right in a row to pass the turn to the next player 
- If your answer is right, the card will turn around and you choose the next card (or it is the turn of the next player)
    - However if your answer is wrong, you take the card + all the connected cards as minus points on top of that you have to try again to get 2 answers in a row right.

<br>
7. The users of the game win by turning around every card on the board

## Project Structure

The project is organized as follows:

### Folder Details:
- **/Data**: Data includes data objects. 
    - Now only includes **SquaringData** scriptable object

- **/Events**: Each folder includes events. Folder names correspond with the places where the events are used

- **/Materials/Grid**: For now there are only materials for gridtiles

- **/Prefabs**: Houses reusable game objects such as cards, the deck, and other UI components.

    - **/Prefabs/Cards**: Houses prefabs for: Cards, CardBoardHandler, DiscardStack, CardStack, PeekedCardUI and SmallPeekedCardUI

        - PeekedCardUI and SmallPeekedCardUI are not used in SquaringGame but could be interesting for other games

    - **/Prefabs/Grid**: Houses prefabs for Grid: GridGenerator, NotUsedTile and PlayableTile

        - If you want to add new types of gridtiles, watch at how the tile prefabs look.

    - **/Prefabs/Scene**: Houses just the SceneManager

    - **/Prefabs/Turn**: Houses just the turnSystem

    - **/Prefabs/UI**: Houses UI prefabs for higher / lower buttons, inbetween / outside buttons, PlayerNames and the whole NameField UI of the StartScreen 

- **/Scenes**: Includes Unity scenes for the StartScreen and MainScene.

- **/Scripts**: Contains the main gameplay logic, including scripts for card behaviors, decks, and game flow. I will go more into details in the [Scripts Overview](#scripts-overview) section.

    - **/Scripts/Attributes**: Contains attribute scripts
    - **/Scripts/Cards**: Contains all card scripts
    - **/Scripts/Events**: Contains all event scripts 
        - **/Scripts/Events/GameEventListeners**: Contains all GameEventListenerScripts
            - **/Scripts/Events/GameEventListeners/Typed**: Contains all TypedGameEventListenerScripts
        - **/Scripts/Events/GameEvents**: Contains all GameEventScripts
            -  **/Scripts/Events/GameEvents/Typed**: Contains all TypedGameEventsScripts
    - **/Scripts/Grid**: Contains all grid scripts
    - **/Scripts/Scene**: Contains all scripts for managing scenes
    - **/Scripts/Turn**: Contains all script for turn system
    - **/Scripts/UI**: Contains all UI scripts
        - **/Scripts/UI/PlayerScreen**: Contains all PlayerScreenUI scripts
    - **/Scripts/Utils**: Contains extension scripts

- **/TextMeshPro**: Contains assets from TextMeshPro, used for rich text rendering and UI elements. (This folder is directly imported from TextMeshPro)

## Scripts Overview
### Attributes
- **/Scripts/Attributes/ExpandableAttribute**: Allows editor to draw on expandable

### Cards
- **/Scripts/Cards/Card**: Attach this script to card prefab. Card values can be set through editor and the UI text of the card values will automatically change with it. Only used for setting color and symbol of card
- **/Scripts/Cards/CardBoardHandler**: When the grid is made, the cardboardhandler makes the starting board. The cardboardhandler checks for empty tiles and invokes them so the cardstack can put cards down. The cardboardhandler checks which cards can be turned around (card needs at least 1 neighbour). The cardboard handler has functions to check neighbours: GetOpenCardNeighbourValues to get the values of all open neighbours of a card, CardComparison to compare cards with each other and retrieve a right / wrong answer from it. a function to getneighbours of a card and a function to get allConnectedTurnedAround cards when someone answered wrong and those need to be removed. It handles higher/lower/inside/outside clicks and decides wrong / right answer by this. It checks if the players have won the game. 
- **/Scripts/Cards/CardStack**: Holds functions to call shuffle stack, deal cards and take card from discardstack
- **/Scripts/Cards/DiscardStack**: Holds functions to retrieve cards from game, able to send cards to CardStack
- **/Scripts/Cards/InteractableCard**: Holds functions to turn around cards to which this script is attached to, amount of neighbours are set using CardBoardHandler

### Events
- **/Scripts/Events/IUnityEventCallback**: Exposes unity event based callbacks
- **/Scripts/Events/IUnityEventCallbackT**: Exposes unity event based callbacks using T as value type used in the callback
- **/Scripts/Events/TypedUnityEvents**: Holds classes for all typed unity events
    - **/Scripts/Events/GameEventListeners/DelayedGameEventListener**: Used when there is need of a listener with delay
    - **/Scripts/Events/GameEventListeners/GameEventListener**: Script to add GameEvent functionality to an object
    - **/Scripts/Events/GameEventListeners/GameEventListenerBase**: Implements a generic base class to listen to game events and respond with Unity Events
    - **/Scripts/Events/GameEventListeners/IGameEventListener**: Script to add IGameEvent functionality to an object
    - **/Scripts/Events/GameEventListenersIGameEventListenerT**: Adds listener functionality to an object, T is type of value to pass through
        - **/Scripts/Events/GameEventListeners/Typed**: Contains all GameEventListeners for when there is need of passing variables of certain types with event. Current supported types: Bool, Button, Float, GameObject, GameObjectList, Int, IntList, String, StringList and Vector 2
    - **/Scripts/Events/GameEvents/GameEvent**: Script to make gameEvent scriptable objects
    - **/Scripts/Events/GameEvents/GameEventBase**: Implements a generic base class to handle game events 
    - **/Scripts/Events/GameEvents/IGameEvent**: Game wide event based on scriptableobjects, can be used from the editor in conjunction with IGameEventListener 
    - **/Scripts/Events/GameEvents/IGameEventT**: Game wide event based on scriptableobjects, can be used from the editor in conjunction with IGameEventListener , T is type of value to pass 
        -  **/Scripts/Events/GameEvents/Typed**: Contains all GameEvents for when there is need of passing variables of certain types with event. Current supported types: Bool, Button, Float, GameObject, GameObjectList, Int, IntList, String, StringList and Vector 2

### Grid
- **/Scripts/Grid/GridData**: Scriptable object which holds playable tiles, gridX and Ysize. Marks indices for grid already so that gridgenerator can generate the tiles. 
- **/Scripts/Grid/GridGenerator**: Generates gridtiles based on predefined data (GridData). It triggers an event when the grid is created. 
- **/Scripts/Grid/GridTileTypes**: Holds all possible gridtile types
- **/Scripts/Grid/NotUsedTile**: Attach to not used tile so that it can be identified as not used tile
- **/Scripts/Grid/PlayableTile**: Attach to playable tile so that it can be identified as playable tile

### Scene
- **/Scripts/Scene/SceneSwitcher**: Basic script for switching scenes. Holds function for reloading scene, switching scene with delay or switch scene immediately.

### Turn
- **/Scripts/Turn/PlayerTurnHandler**: Holds functions for a player's turn like: Reset amount of played cards, add played cards to the amount and check if the amount of cards needed to play is reached.
- **/Scripts/Turn/TurnSystem**: Sets a random startingplayer and invokes event to set currentplayer when player changes. Holds minus points for each player

### UI
- **/Scripts/UI/ChangeTextColorThroughEvent**: On event 1 set text color to color 1, on event 2 set text color to color 2
- **/Scripts/UI/SetActiveOnEvent**: On event set object active, boolean can be turned off so that object will be set non-active
- **/Scripts/UI/SetActiveOnEvents**: On multiple events set object active, boolean can be turned off so that object will be set non-active
- **/Scripts/UI/SetActiveOnObjectCount**: If x amount of objects from list are active, set another object active
- **/Scripts/UI/SetActiveOnTime**: Sets object active after timer, timer can start on enable or after event received, boolean can be turned off so that object will be set non-active
- **/Scripts/UI/SetEmptyStringOnCountdown**: After receiving event, sets string to empty string after the countdown
- **/Scripts/UI/SetNumberInStrings**:  Set number inbetween 2 strings or on the end or start of a string. Activates on an Event using intgame event
- **/Scripts/UI/SetReverseActive**: Set active objects non-active, set non-active objects active
- **/Scripts/UI/SetScoreText**:  Sets score text on an event. Gets names and scoretext from playerinfoholder
- **/Scripts/UI/SetStringOnEvents**: Sets text on one of the events. You can add a delay in editor
- **/Scripts/UI/SetStringOnStringEvent**: Sets text on one of the events. Receives text from event and you can add text behind it.
- **/Scripts/UI/SetTextColorOnEvent**: This script is used to set text color from another text, by determining what color specific strings of a text should be. 
- **/Scripts/UI/SetTextFromIntListEvent**: Set text from int list event, not modular yet. List count determines texts around the numbers, which are retrieved from the list. 
- **/Scripts/UI/SetTextOnEvents**: Set text(s) on event (text is pushed with event)
- **/Scripts/UI/PlayerScreen/NameInputFieldScript**: Script which submits text fields.
- **/Scripts/UI/PlayerScreen/OnAddButtonPressed**: Script for the add button of the player screen. Retrieves button and passes it on click
- **/Scripts/UI/PlayerScreen/PlayerInfoHolder**: Holds names for players, carries over through scenes
- **/Scripts/UI/PlayerScreen/SendStringFromTMPs**:  Fires editor assigned string list through event
- **/Scripts/UI/PlayerScreen/SetNextInListActive**: Set next non-active object in list active

### Utils
- **/Scripts/Utils/StackExtensions**: Has 2 stack extensions: Make a stack a gameobjectlist and shuffle the stack.

## Contributing

Thank you for considering contributing to this project! Contributions are welcome and encouraged. To get started, please follow these steps:

### How to Contribute

1. **Fork the Repository**  
   Click the "Fork" button at the top-right corner of this repository to create your own copy.

2. **Clone the Repository**  
   Clone your forked repository to your local machine:
   git clone https://github.com/Tig709/SquaringGame.git

3. **Create a Branch**
    Create a new branch to work on a specific feature or fix:
    - git checkout -b feature-or-fix-name

4. **Make Changes**
Add or update code, assets, or documentation as needed.

5. **Commit Your Changes**
Write clear and descriptive commit messages:

6. **Push Changes to Your Fork**
Push the changes to your forked repository:

7. **Submit a Pull Request**
Open a pull request to the main repository. Provide a detailed description of your changes and why they are necessary.

### Guidelines 
- Follow the existing coding style and conventions used in the project.
- Document your code where applicable.
- Ensure that your changes do not break existing functionality.
- Test your additions thoroughly before submitting.

### Issues 
If you encounter a bug or have a feature request, please open an issue in the Issues tab of this repository. Be sure to include:
- A clear and descriptive title.
- Steps to reproduce the issue (if applicable).
- Any relevant screenshots or error messages.

We look forward to your contributions!

## License

This project is licensed under the MIT License. See the full license details below:

MIT License

Copyright (c) 2024 [Tigo Stam]

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

