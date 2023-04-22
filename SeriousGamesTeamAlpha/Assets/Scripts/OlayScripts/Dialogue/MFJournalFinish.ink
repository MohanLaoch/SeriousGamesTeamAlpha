EXTERNAL CloseGame()
VAR ManyWords = false

#speaker: MFGirl #emotion: MF_Neutral

{ManyWords == false : -> NotAsMuch | -> ExtraOpener}


===ExtraOpener==
Wow you really <color=red>wrote a lot on the page!</color>

There's a lot to unpack and hopefully your mind is at ease after writing! 

And if not, that's ok!, you've still managed to get a lot down!


->main



===NotAsMuch===
Not much to write there? That's no worries! 

Just as long as that's what you need to write Hopefully, your mind is at ease! 

And if not, that's ok, you've still completed the exercise!

->main
===main===
Thank you for trying out my Mindfulness Programme! <color=red>Feel free to comeback to repeat the programme! </color> #emotion: MF_Excited

Have you felt positively since starting the exercise? #emotion: MF_Neutral

+[Yes]
-> YESOPTION
+[NO]
->NOOPTION

===YESOPTION===
Then I'm glad my programme has managed to work for you, throughout all this! #emotion: MF_Excited
I look forward to seeing you back here again! 
~CloseGame()
->END


===NOOPTION===
I'm a little disappointed that it hasn't worked for you #emotion: MF_Sad
However! #emotion: MF_Neutral
Perhaps the next time around it'll be a more pleasent experience for you!
~CloseGame()
->END