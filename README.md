# FPS-Character-Controller

The FPS Character controller used in my game, The Interview (https://tyrobyte.itch.io/the-interview).

# License

This Character Controller is free to use for all purposes, commercial or personal. Credit should be given if modified upon.

# Dependencies

- Free Footsteps System By Pavel Cristian (https://assetstore.unity.com/packages/tools/audio/free-footsteps-system-47967#description).

# How To Use

- The Player GameObject must be set up with the following hierarchy:
  - Root ( > Pivot > Main Camera > Weapons (Optional) > Your Weapon GameObjects (Optional)
  ![Screenshot 2022-06-04 202552](https://user-images.githubusercontent.com/63674376/172018544-349014db-ecd5-4688-8d16-71165c73a777.png)

- The Root GameObject (Or the Player GameObject) should have the following attached to it:
  - Character Controller
  - Character FootSteps Script (From Free Footsteps System Asset)
  - Sensitivity Controller Script (Optional)

- A GameObject (For Ex: Cursor Manager) should be created and the Cursor Manager Script can be applied on it.
  - Set the Lock Mode to Locked and the Visible bool to False.

# Example Values For Setup
![Screenshot (910)](https://user-images.githubusercontent.com/63674376/172017951-0614cd58-ccef-4db9-9ade-a8fbbafff87f.png)
![Screenshot 2022-06-04 202428](https://user-images.githubusercontent.com/63674376/172018466-4affd308-6a2b-4c4d-b93a-8b94ee09257e.png)
