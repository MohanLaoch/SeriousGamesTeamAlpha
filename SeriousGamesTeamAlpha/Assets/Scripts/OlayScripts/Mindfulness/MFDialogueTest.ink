INCLUDE CharacterData.ink
EXTERNAL openJournal()


Hi There! #audio: 0

#speaker: Olivia
#emotion: MF_Neutral
My name is {name} and welcome to the <color={HC}> Mindfulness Programme! </color> #audio: 1

->main

===main===
Here we will do plenty of <color={HC}>breathing</color> and <color={HC}>physical exercises</color> as well as some <color={HC}>mindful exercises! </color> #audio: 2
Are you excited for it? #audio: 3

+[Yes]
    Yes! That's the spirit! #emotion: MF_Excited #audio: 4
    ->BreathingMain
    
+[No]
    Aww! Try it and see how you feel afterwards. #emotion: MF_Sad #audio: 5
    ->BreathingMain
    


===BreathingMain==

Let's get started with the first breathing exercise. #emotion: MF_Neutral #audio: 6
Firstly, <color={HC}> I want you to breathe in for about 5 seconds! </color> #audio: 7
Then <color={HC}> I want you to  breathe out for another 5 seconds! </color>  #audio: 8

Breathe in! #delay: 5 #audio: 9

Breathe out! #delay: 5 #audio: 10

That's the breathing done! How do you feel? #audio: 11

+[Great]
    That's good to hear! I'm sure you're well relaxed now. #emotion: MF_Excited #audio: 12
    ->StretchingSection
+[Tired]
    Don't worry, you're just not used to the exercises yet. #emotion: MF_Sad #audio: 13
    You can try again on your own before hitting the continue button! #emotion: MF_Neutral #audio: 14
    ->StretchingSection

===StretchingSection===
Now let's move onto the <color={HC}>Stretches!</color> #emotion: MF_Neutral #audio: 15

We're just going to do a simple <color={HC}> stretch pose and I'm going need you to hold onto it for a few seconds.</color> #audio: 16

Just follow my pose. Like this! #pose: MF_Stretch_01 #audio: 17

Now I want you to <color={HC}>hold this for 10 seconds!</color> #audio: 18

Go! #delay: 10 #audio: 19

Done! #audio: 20

That was good! I'm sure you feel nice and stretched. Those muscles feeling better already? #audio: 21  #emotion: MF_Neutral 

+[Absolutely]
    They're about to feel even better after this next stretch! #emotion: MF_Excited #audio: 22
    -> StretchingSection2
+[Worse]
    They're worse!? #emotion: MF_Sad #audio: 23
    Well, they're about to feel even better after this next stretch! #emotion: MF_Neutral #audio: 24
    -> StretchingSection2

===StretchingSection2===
Now I want you to <color={HC}>copy this pose!</color> #pose: MF_Stretch_02 #audio: 25
Hold this for <color={HC}> 8 seconds!</color> #audio: 26
Go! #delay: 8 #audio: 27

I'm already feeling a lot better. My muscles are relaxed now. I hope yours are too! #emotion: MF_Neutral #audio: 28

+[They are]
    I'm glad that my training programme is working for you! #emotion: MF_Excited #audio: 29
    ->StretchingSection3
+[They aren't]
    Perhaps the next stretch will make them better for you. #emotion: MF_Sad #audio: 30
    ->StretchingSection3
    
    
===StretchingSection3===
Okay. This is our last pose! #emotion: MF_Neutral #audio: 31

I need you to <color={HC}>hold this pose for 5 seconds! </color> #pose: MF_Stretch_03 #audio: 32

Now let's see that stretch! #delay: 5 #audio: 33

That's good you can stop it there! #emotion: MF_Neutral #audio: 34

How do you feel? #audio: 35

+[Really Good!]
    That's great to hear! I'm glad that stretch went well for you. #emotion: MF_Excited #audio: 36
    -> Closing
+[Really Bad!]
    That's unfortunate. Feel free to try again in your own time. It gets easier with practice! #emotion: MF_Sad #audio: 37
    ->Closing

===Closing===
That's all for the Stretching exercises. How are you feeling? #emotion: MF_Neutral #audio: 38
+[Good]
    I'm glad to hear that the stretches have worked well for you! With your muscles now rested you can freely move to the next exercise. #emotion: MF_Excited #audio: 39
    -> NextClosing
+[Bad]
    I'm sorry about that. You can try the exercises again if you couldn't get them right the first time. If you don't want to, you're free to move onto the next exercise. #emotion: MF_Sad #audio: 40
    ->NextClosing
    
===NextClosing===
Now for our final exercise we are going to <color={HC}> write messages into a small diary </color>. #emotion: MF_Neutral #audio: 41

I want you to either <color={HC}> write a list of encouraging words to yourself, or you can write down your current thoughts and feelings onto the page</color>! #audio: 42

Whether it's your anxieties or your current struggles, you're free to write down anything as you please, don't be afraid to write it all down! #audio: 43

When you're finished writing in your journal, <color={HC}> Press the Enter Key to submit your journal!</color> #audio: 44

~openJournal()    

-> END
