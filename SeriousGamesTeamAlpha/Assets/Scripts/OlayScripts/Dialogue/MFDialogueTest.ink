INCLUDE CharacterData.ink
EXTERNAL openJournal()


Hi There!

#speaker: Olivia
#emotion: MF_Neutral
My name is {name} and welcome to the <color={HC}> Mindfulness Programme! </color> #audio: 0

->main

===main===
Here we will do plenty of <color={HC}>breathing</color> and <color={HC}>physical exercises</color> as well as some <color={HC}>mindful exercises! </color> #audio: 1
Are you excited for it? #audio: 2

+[Yes]
    Yes! That's the spirit! #emotion: MF_Excited #audio: 3
    ->BreathingMain
    
+[No]
    Aww! Try it and see how you feel afterwards. #emotion: MF_Sad #audio: 4
    ->BreathingMain
    


===BreathingMain==

Let's get started with the first breathing exercise. #emotion: MF_Neutral #audio: 5
Firstly, <color={HC}> I want you to breathe in for about 5 seconds! Then I want you to  breathe out for another 5 seconds! </color>  #audio: 6

Breathe in! #delay: 5 #audio: 7

Breathe out! #delay: 5 #audio: 8

That's the breathing done! How do you feel? #audio: 9

+[Great]
    That's good to hear! I'm sure you're well relaxed now. #emotion: MF_Excited #audio: 10
    ->StretchingSection
+[Tired]
    Don't worry, you're just not used to the exercises yet. #emotion: MF_Sad #audio: 11
    You can try again on your own before hitting the continue button! #emotion: MF_Neutral #audio: 12
    ->StretchingSection

===StretchingSection===
Now let's move onto the <color={HC}>Stretches!</color> #emotion: MF_Neutral #audio: 13

We're just going to do a simple <color={HC}> stretch pose and I'm going need you to hold onto it for a few seconds.</color> #audio: 14

Just follow my pose. Like this! #pose: MF_Stretch_01 #audio: 15

Now I want you to <color={HC}>hold this for 10 seconds!</color> #audio: 16

Go! #delay: 10 #audio: 17

Done! #audio: 18

That was good! I'm sure you feel nice and stretched. Those muscles feeling better already? #audio: 19  #emotion: MF_Neutral 

+[Absolutely]
    They're about to feel even better after this next stretch! #emotion: MF_Excited #audio: 20
    -> StretchingSection2
+[Worse]
    They're worse!? #emotion: MF_Sad #audio: 21
    Well, they're about to feel even better after this next stretch! #emotion: MF_Neutral #audio: 22
    -> StretchingSection2

===StretchingSection2===
Now I want you to <color={HC}>copy this pose!</color> #pose: MF_Stretch_02 #audio: 23
Hold this for <color={HC}> 8 seconds!</color> #audio: 24
Go! #delay: 8 #audio: 25

I'm already feeling a lot better. My muscles are relaxed now. I hope yours are too! #emotion: MF_Neutral #audio: 26

+[They are]
    I'm glad that my training programme is working for you! #emotion: MF_Excited #audio: 27
    ->StretchingSection3
+[They aren't]
    Perhaps the next stretch will make them better for you. #emotion: MF_Sad #audio: 28
    ->StretchingSection3
    
    
===StretchingSection3===
Okay. This is our last pose! #emotion: MF_Neutral #audio: 29

I need you to <color={HC}>hold this pose for 5 seconds! </color> #pose: MF_Stretch_03 #audio: 30

Now let's see that stretch! #delay: 5 #audio: 31

That's good you can stop it there! #emotion: MF_Neutral #audio: 32

How do you feel? #audio: 33

+[Really Good!]
    That's great to hear! I'm glad that stretch went well for you. #emotion: MF_Excited #audio: 34
    -> Closing
+[Really Bad!]
    That's unfortunate. Feel free to try again in your own time. It gets easier with practice! #emotion: MF_Sad #audio: 35
    ->Closing

===Closing===
That's all for the Stretching exercises. How are you feeling? #emotion: MF_Neutral #audio: 36
+[Good]
    I'm glad to hear that the stretches have worked well for you! With your muscles now rested you can freely move to the next exercise. #emotion: MF_Excited #audio: 37
    -> NextClosing
+[Bad]
    I'm sorry about that. You can try the exercises again if you couldn't get them right the first time. If you don't want to, you're free to move onto the next exercise. #emotion: MF_Sad #audio: 38
    ->NextClosing
    
===NextClosing===
Now for our final exercise we are going to <color={HC}> write messages into a small diary </color>. #emotion: MF_Neutral #audio: 39

I want you to either <color={HC}> write a list of encouraging words to yourself, or you can write down your current thoughts and feelings onto the page</color>! #audio: 40

Whether it's your anxieties or your current struggles, you're free to write down anything as you please, don't be afraid to write it all down! #audio: 41

When you're finished writing in your journal, <color={HC}> Press the Enter Key to submit your journal!</color> #audio: 42

~openJournal()    

-> END
