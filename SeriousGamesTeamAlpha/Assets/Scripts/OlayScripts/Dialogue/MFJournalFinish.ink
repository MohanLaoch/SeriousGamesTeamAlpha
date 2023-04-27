INCLUDE CharacterData.ink
EXTERNAL CloseGame()


VAR ManyWords = false

#speaker: Olivia #emotion: MF_Neutral

{ManyWords == false : -> NotAsMuch | -> ExtraOpener}


===ExtraOpener==
Wow you really <color={HC}>wrote a lot on the page!</color> #audio: 43

It's good to unpack, hopefully your mind is at ease after writing! #audio: 44

If not, that's okay, you've still managed to get a lot down. #audio: 45


->main



===NotAsMuch===
Not much to write there? That's no problem! #audio: 46

Hopefully your mind feels more at ease. #audio: 47

If not, that's okay, you've still completed the exercise! #audio: 48

->main
===main===
Thank you for trying out my Mindfulness Programme! <color={HC}>Feel free to comeback anytime! </color> #emotion: MF_Excited #audio: 49

Have you felt positively since starting the exercise? #emotion: MF_Neutral #audio: 50

+[Yes]
-> YESOPTION
+[No]
->NOOPTION

===YESOPTION===
That's great! I'm glad my programme has managed to work for you. #emotion: MF_Excited #audio: 51
I look forward to seeing you back again! #audio: 52
~CloseGame()
->END

===NOOPTION===
That's unfortunate that it hasn't worked for you. #emotion: MF_Sad #audio: 53
However! #emotion: MF_Neutral #audio: 54
Perhaps the next time around it'll be a more pleasant experience! #audio: 55
~CloseGame()
->END