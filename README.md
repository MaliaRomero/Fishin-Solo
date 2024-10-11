# Fishin Solo
 
Playable Build:

https://maliaromero.github.io/Fishin-Solo/


How to play:

Draw a card from any deck by pushing one of the deck buttons. The deeper the depth, the more bait it costs but there is a higher chance to get better cards. The shallow costs 1 bait, deep costs 2, and unfathomable costs 3 bait, and you can only draw if you have enough bait. 

Discard one card by clicking on it, this is how you will be able to get more bait (Option to skip discard coming soon). Once a card is discarded, an event will play (debug log for now), and if you are able to, you can draw again and repeat!


Reformatted the game to be single player. Added the following elements:

-Cards hold an integer for trophy points and bait points that is displayed on card

-Player has starting bait of 3

-Players can only draw from a deck if they have enough bait

-Trophy point counter that increments when cards enter hand and decrease when card is discarded

-Have cards increase bait counter according to bait point on cards when discarded

-UI elements should scale better to screen, still needs testing

-Have a fully functioning discard system

-Implemented a turn sequence that player must take in order
