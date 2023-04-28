INCLUDE CharacterData.ink
EXTERNAL CloseGame()




#speaker: Olivia #emotion: MF_Neutral


->NotAsMuch

===NotAsMuch===
Not much to write there? That's no problem! #audio: 48 

Hopefully your mind feels more at ease. #audio: 49

If not, that's okay, you've still completed the exercise! #audio: 50

->main
===main===
Thank you for trying out my Mindfulness Programme! <color={HC}>Feel free to comeback anytime! </color> #emotion: MF_Excited #audio: 51

Have you felt positively since starting the exercise? #emotion: MF_Neutral #audio: 52

+[Yes]
-> YESOPTION
+[No]
->NOOPTION

===YESOPTION===
That's great! I'm glad my programme has managed to work for you. #emotion: MF_Excited #audio: 53
I look forward to seeing you back again! #audio: 54
~CloseGame()
->END




===NOOPTION===
That's unfortunate that it hasn't worked for you. #emotion: MF_Sad #audio: 55
However! #emotion: MF_Neutral #audio: 56
Perhaps the next time around it'll be a more pleasant experience! #audio: 57
~CloseGame()
->END


