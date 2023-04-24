INCLUDE CharacterData.ink
EXTERNAL CloseGame()


VAR ManyWords = false

#speaker: Olivia #emotion: MF_Neutral

{ManyWords == false : -> NotAsMuch | -> ExtraOpener}


===ExtraOpener==
Wow you really <color={HC}>wrote a lot on the page!</color>

It's good to unpack, hopefully your mind is at ease after writing! 

If not, that's okay, you've still managed to get a lot down.


->main



===NotAsMuch===
Not much to write there? That's no problem! 

Hopefully your mind feels more at ease. 

If not, that's okay, you've still completed the exercise!

->main
===main===
Thank you for trying out my Mindfulness Programme! <color={HC}>Feel free to comeback anytime! </color> #emotion: MF_Excited

Have you felt positively since starting the exercise? #emotion: MF_Neutral

+[Yes]
-> YESOPTION
+[No]
->NOOPTION

===YESOPTION===
That's great! I'm glad my programme has managed to work for you. #emotion: MF_Excited
I look forward to seeing you back again! 
~CloseGame()
->END

===NOOPTION===
That's unfortunate that it hasn't worked for you. #emotion: MF_Sad
However! #emotion: MF_Neutral
Perhaps the next time around it'll be a more pleasant experience!
~CloseGame()
->END