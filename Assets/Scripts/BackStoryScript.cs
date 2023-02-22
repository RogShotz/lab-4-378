using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackStoryScript : MonoBehaviour
{
    public List<string> script = new List<string>
    {
        "A cell phone rings. It's a call from an unknown number from the same area code.\n…\n\"Hello?\"",
        "\"Hi! Is this Alex?\" asks the upbeat voice on the other end.\n\"This is Eugenia, I work with your mom. Do you remember me?\nI haven't seen you since you were little.\"",
        "\"Oh, hey! Yeah, of course I remember you,\" Alex responds. \"What can I do for you?\"",
        "Eugenia takes a deep breath.\n\"Well! You see, I have a son now, Eugene, he's two years old.\nGosh, I remember when you were that young! And now you're twenty! Time sure does fly.\nAnyway, you're going to love him.\nSomething urgent came up and I'm going to need to be out of town all day tomorrow.\nEugene's last babysitter quit unexpectedly after only watching him once! Can you believe that?!\"",
        "Eugenia lets out an exasperated sigh. After a brief pause, she continues,\n\"Your mom suggested I call you.\nShe said you don't go to work anymore, and you just do your online art thing and you stay in your apartment all day.\nCould you please watch Eugene tomorrow?\nI normally do $25, but since this is so last minute I'll pay you $35 an hour.\"",
        "Alex was taken aback. \"Uh… I mean, I do work. My 'online art thing' keeps me pretty busy, I have a bunch of commissions I'm working on right now. But, uh..\"",
        "\"Well, that's great! I'm happy to hear it's going so well for you! You've always been so talented,\" Eugenia interjects.",
        "Alex continues, \"But, uh, I don't know anything about babysitting actual babies though. I guess he'd be a toddler.\"\nIt's clear from Alex's tone of voice how apprehensive she is about doing this.\nShe doesn't dislike kids particularly, but she doesn't know this one.\nIt also just sounds like a lot to deal with, even only for a day.\n\"Plus, uh, like I said, I just have a ton of art commissions I need to work on right now.\"",
        "Eugenia doesn't miss a beat. \"No, don't worry! He's an easy kid. He has his iPad games, they always keep him busy. He's even potty trained!\nI just need you to be there to make sure he doesn't get hurt.\nYou can even bring your pencils and paper over and work on your commissions at my house while you watch him!\nPlease! I really need some help here. I would appreciate it SO much.\"",
        "Alex hesitates, but ultimately agrees to watch the kid. It doesn't sound too unbearable. Plus she's always liked helping other people.",
        "Alex heads over to Eugenia's house the next day. She could have driven, but it's a nice day and they don't live too far from each other, so she skateboards instead. She knocks on the door and Eugenia lets her in.",
        "\"Wow, Alex, I almost didn't recognize you with that bright red hair. I remember how pretty and blonde it was when you were younger. Oh, you even got a nose ring too.\"",
        "Alex stares at her.",
        "\"It looks good though!\" Eugenia adds quickly.",
        "Awkward.",
        "After introducing Alex to Eugene and showing her around the house, Eugenia takes off.",
        "Eugene seems like a nice enough kid. His mom was right.\nHe's pretty low maintenance.\nHe just sits and stares at that screen for hours.",
        "In the meantime, Alex gets a lot of work done on her own tablet.\nEverything is going great.\nThis is a much easier day than expected.",
        "\"The battery's dead,\" Eugene says.",
        "Uh-oh. Where's the charger for that thing? His mom didn't say where it was. Eugene doesn't seem to know either. Alex didn't even bring a USB-C charger for her own phone.",
        "\"Can I use your iPad?\" Eugene asks.",
        "Alex doesn't use an iPad. But even if she did, Eugene's is in horrible condition.\nIt's scratched everywhere and it's weirdly sticky, it looks like it's covered in snot and drool.\nAlex considers herself a nice person, but there's no way she'd let this kid borrow any of her things.",
        "\"I'm still using it, okay?\" she says gently, \"Let's call your mom and see where we can find your charger.\"",
        "\"No!! I wanna use your iPad!\" Eugene starts to scream as his eyes water.",
        "What the hell is happening right now, Alex thinks to herself.",
        "He starts crying uncontrollably. She tries to calm him down but he doesn't want to. His eyes are bloodshot red.",
        "He gets frustrated and walks over to the tv. He picks up a remote and throws it.",
        "He wanders around the living room throwing whatever he can get his little hands on.\nA cup full of water.\nThe tiny books on the coffee table.\nA small house plant.",
        "Alex tries to stop him but every time she tries to pick him up he screams even louder and gets more aggressive.",
        "Somehow, he even picks up a chair and throws it at Alex, which she dodges.\nAlex was both impressed at her own dodging ability and at the raw strength of this terrifying bloodthirsty toddler.",
        "Outdoing his previous feat, Eugene then throws the entire dining room table, taking out a window and a huge chunk of the wall.",
        "Alex doesn't like where this is going. With her skateboard in hand, she runs out of the house's newly opened exit and waits outside.\nShe can still see Eugene clearly from where she's standing.",
        "Eugene furiously exits the house and approaches her, anger in his eyes. This is dangerous, she thinks. She didn't even want to do this job. She didn't need it in the first place. This was just a favor to a family friend.",
        "The logical solution would be to flee. Alex knows this. But she still feels responsible.\nThe same sense of responsibility that keeps her on track with all of her art commissions tells her that she needs to finish this job, and she has to finish it correctly, with a safe, unhurt toddler.",
        "She gets on her skateboard and starts riding away from Eugene. He continues to follow her. It would be easy to lose him, but she wants to make sure he stays in sight. ",
        "If she keeps making him run around, eventually he's gotta get tired out, right? ",
        "Objects that he walks past are now exploding or bursting into flames. He even walks through a moving car, destroying it.",
        "She begins to think she might not have to worry too much about his safety after all…\nBut, even so, she knows she has a job to do."
    };
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI pageNumberTextMeshPro;
    public int i = 0;
    public int len;

    void Start()
    {
        len = script.Count;
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = script[i];
        SetPageNumber();
    }

    public void PreviousPassage()
    {
        if (i > 0) {
            i--;
            textMeshPro.text = script[i];
            SetPageNumber();
        }
    }

    public void NextPassage()
    {   
        if (i < len - 1) {
            i++;
            textMeshPro.text = script[i];
            SetPageNumber();
        }
    }

    public void SetPageNumber()
    {
        pageNumberTextMeshPro.text = (i + 1) + " / " + len;
    }
}
