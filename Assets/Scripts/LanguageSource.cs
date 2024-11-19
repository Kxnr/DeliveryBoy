using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LanguageSource
{
    public enum LANGUAGE { ENGLISH, GERMAN };
    public static LANGUAGE current_language;

    private const string GERMAN_TRANSLATION_NEEDED = "GERMAN TRANSLATION NEEDED";

    // TODO: JPB: Needs German translations
    private static Dictionary<string, string[]> language_string_dict = new Dictionary<string, string[]>()
    {
        {"", new string[] {"", ""}},

        { "final recall", new string[] {"All items exhausted. Press any key to proceed to final recall.", "Alle Gegenstände ausgeliefert. Weiter mit beliebiger Taste."} },
        { "final no recall", new string[] {"All items exhausted. Press any key to proceed.", "Alle Gegenstände ausgeliefert. Drücken Sie eine beliebige Taste, um fortzufahren."} },
        { "store cue recall", new string[] {"Please recall which item you delivered to the store shown on the screen." + "\n\n" + 
                                            "If you can't remember the item, please guess an item, and press the (B) button immediately afterwards to indicate it was a guess.", 
                                            "Bitte nennen Sie den Gegenstand, den Sie zu dem gezeigte Geschäft geliefert haben." + "\n\n" + 
                                            "Wenn Sie sich nicht an den Gegenstand erinnern können,raten Sie bitte einen Gegenstand und drücken Sie unmittelbar danach die Taste (B), um anzuzeigen, dass Sie geraten haben."} },
        { "store cue recall no reject", new string[] {"Please recall which item you delivered to the store shown on the screen.",
                                            "Bitte nennen Sie den Gegenstand, den Sie zu dem gezeigte Geschäft geliefert haben."} },
        { "day objects recall", new string[] {"After the beep, please recall all items from this delivery day.", "Bitte nennen Sie nach dem Signalton möglichst viele Gegenstände, die Sie an diesem Liefertag geliefert haben."} },
        { "microphone test", new string[] {"Microphone Test", "Mikrofontest"} },
        { "next store prompt", new string[] {"The next target store is the ", "Das nächste Geschäft ist: "} },
        { "next package prompt", new string[] {"The next item has to be delivered to the ", "Ziel der nächsten Lieferung: " } },
        { "rating improved", new string[] {"Your rating improved!", "Ihre Wertung hat sich verbessert!"} },
        //{ "you now have", new string[] {"You now have points: ", "Aktuelle Punktzahl: "} },
        //{ "you earn points", new string[] {"You earned points: ", "Gesammelte Punkte: "} },
        { "continue", new string[] {"Press (X) to continue.", "Drücken Sie (X), um fortzufahren."} },
        { "start", new string[] { "Press (X) to start.", "Drücken Sie (X), um zu beginnen." } },
        { "no continue", new string[] {"", ""} },
        { "please point", new string[] {"Please point to the ", "Bitte richten Sie den Pfeil aus auf: "} },
        { "joystick", new string[] {"Use the joystick to adjust the arrow, then press (X) to continue.", "Verwenden Sie den Joystick, um die Richtung des Pfeils einzustellen. Drücken Sie anschließend (X), um fortzufahren."} },
        { "keyboard", new string[] {"Use Arrow keys to adjust the arrow, then press (X) to continue", "Verwenden Sie die Pfeiltasten, um die Richtung des Pfeils einzustellen. Drücken Sie anschließend (X), um fortzufahren."} },
        { "wrong by", new string[] {"Not quite. The arrow will now show the exact direction. That was off by degrees: ", "Nicht ganz! Der Pfeil zeigt Ihnen nun die richtige Richtung. Abweichung in Grad: "} },
        { "correct to within", new string[] {"Good! That was correct to within degrees: ", "Fast perfekt! Abweichung in Grad: "} },
        { "incorrect pointing", new string[] {"Not quite. ", "Nicht ganz."} },
        { "correct pointing", new string[] {"Good job! ", "Gut gemacht!"} },
        { "all objects recall", new string[] {"Please recall all the items that you delivered.", "Bitte nennen Sie möglichst viele gelieferte Gegenstände."} },
        { "all stores recall", new string[] {"Please recall all the stores that you delivered items to.", "Bitte nennen Sie alle Geschäfte, zu denen Sie Gegenstände geliefert haben."} },
        { "end message", new string[] {"Thank you for being a great delivery person!", "Vielen Dank für Ihre Teilnahme!"} },
        { "end message scored", new string[] {"Thank you for being a great delivery person! Your cumulative score is: ", "Vielen Dank für Ihre Teilnahme! Ihre Gesamtwertung ist: "} },

        { "standard intro video", new string[] { "Press (Y) to continue, \n Press (N) to replay instructional video.",
                                                 "Drücken Sie (Y) um fortzufahren, \n Drücken Sie (N), um das Video noch einmal zu sehen." } },
        { "two btn efr intro video", new string[] { "Press (Y) to continue to the next delivery day, \n Press (N) to replay instructional video.",
                                                    "Drücken Sie (Y), um die nächste Runde zu starten, \n Drücken Sie (N) um das Video noch einmal zu sehen." } },

        { "nicls movie", new string[] { "Now we will return to the memory task.\nPress (Y) to continue.", "Drücken Sie (Y), um nun mit der Gedächtnisaufgabe fortzufahren." } },

        { "next day", new string[] { "Press (X) to proceed to the next delivery day.", "Drücken Sie (X), um den nächsten Auslieferungstag zu starten." } },
        { "next practice day", new string[] { "Press (X) to proceed to the next practice delivery day.",
                                              "Drücken Sie (X), um die nächste Übungsrunde zu starten." } },

        { "efr reminder title", new string[] { "Reminder!", "Hinweis!"} },
        { "efr reminder main", new string[] { "When recalling items at the end of the delivery day, we want you to vocalize every specific, concrete item that comes to your mind, even if you have already recalled it, or if it was not presented on the most recent delivery day." + "\n\n" +  
                                              "In these cases, press the (B) button after recalling that word, but before recalling the next word", 
                                              "Wenn Sie am Ende eines Liefertages ausgewählte Gegenstände erinnern, möchten wir, dass Sie den Namen jedes spezifischen, konkreten Gegenstands, der Ihnen in den Sinn kommt, aussprechen, auch wenn Sie ihn bereits ausgesprochen haben oder wenn er am letzten Liefertag nicht geliefert worden ist." + "\n\n" + 
                                              "Drücken Sie in diesen Fällen die Taste (B), nachdem Sie den Namen des Gegenstands genannt haben, aber noch bevor Sie das den Namen des nächsten Gegenstands nennen."}},
        { "efr reminder main no reject", new string[] { "When recalling items at the end of the delivery day, we want you to vocalize every specific, concrete item that comes to your mind, even if you have already recalled it, or if it was not presented on the most recent delivery day.",
                                              "Wenn Sie am Ende eines Liefertages ausgewählte Gegenstände erinnern, möchten wir, dass Sie den Namen jedes spezifischen, konkreten Gegenstands, der Ihnen in den Sinn kommt, aussprechen, auch wenn Sie ihn bereits ausgesprochen haben oder wenn er am letzten Liefertag nicht geliefert worden ist." }},

        { "one btn efr intro video", new string[] { "Press (Y) to continue to the next delivery day, \n Press (N) to replay instructional video.",
                                                    "Drücken Sie (Y), um den nächsten Auslieferungstag zu starten, \n Drücken Sie (N), um das Video noch einmal zu sehen." } },

        { "first day main", new string [] { "Let’s start the first delivery day!", "Beginnen wir mit derm ersten Auslieferungstag!" } },
        { "two btn er first day description", new string [] { "Don’t forget to continue pressing the left/right buttons when recalling items at the end of each delivery day.",
                                                              "Vergessen Sie nicht, weiter die links/rechts Knöpfe zu drücken, wenn Sie sich am Ende jedes Auslieferungstags an die Gegenstände erinnern." } },
        { "one btn er first day description", new string [] { "Don't forget to press the B button after recalling an incorrect word when recalling items at the end of each delivery day.",
                                                      "Denken Sie bitte daran, (B) zu drücken, wenn Sie beim Benennen der Gegenstände am Ende eines Auslieferungstags einen falschen Gegenstand genannt haben." } },

        { "frame test start title", new string [] { "Frame Rate Testing ", "Test der Bildwiederholrate" } },
        { "frame test start main", new string [] { "First, we will check if your connection is fast enough to complete this task. \nAs a test, please briefly navigate around this town using the arrow keys.", "Zuerst überprüfen wir, ob Ihre Verbindung schnell genug ist, um diese Aufgabe auszuführen. \nHierfür navigieren Sie bitte kurz durch diese Stadt, indem Sie die Pfeiltasten benutzen." } },
        { "frame test end title", new string [] { "Your average FPS was ", "Ihre durchschnittliche Bildwiederholrate in FPS war "} },
        { "frame test end pass", new string [] { "You passed our initial FPS check! However, if you experienced any significant lag, you will likely take longer than average to complete the task. However, we can only pay a fixed rate for task completion, regardless of time taken." + "\n" +
                                                 "If your connection was strong and you wish to continue, press (X)." + "\n" +
                                                 "Otherwise, close the window to exit the experiment.", "Ihre Verbindung hat unsere Prüfung der Bildwiederholrate bestanden. Falls Sie jedoch eine erhebliche Verzögerung festgestellt haben, werden Sie wahrscheinlich überdurchschnittlich lange brauchen, um die Aufgabe abzuschließen. Wir können trotzdem nur einen Festpreis für die Aufgabenerledigung zahlen, unabhängig von der Zeit, die dafür aufgewendet wird." + "\n" +
                                                 "Wenn Ihre Verbindung ausreichend schenll war und Sie fortfahren möchten, drücken Sie (X)." + "\n" +
                                                 "Schließen Sie andernfalls das Fenster, um das Experiment zu beenden." } },
        { "frame test end fail", new string [] { "Unfortunately, we require a minimum frame rate of 30 per second for our task to run smoothly. \n Please restart the experiment with better computing environment, or return the HIT.",
                                                 "Leider benötigen wir eine minimale Bildwiederholrate von 30 FPS, damit unsere Aufgabe reibungslos abläuft. \n Bitte starten Sie das Experiment mit einer besseren Computerumgebung/Netzwerkverbindung oder geben Sie das HIT zurück. "} },
        { "frame test continue main", new string [] { "Thank you for your participation. \n\nTry to focus on the task and good luck!", "Vielen Dank für Ihre Teilnahme.\nVersuchen Sie nun, sich auf die Aufgabe zu konzentrieren.\n\nViel Glück!" } },


        { "town learning title", new string [] { "Town Learning Phase", "Kennenlernen der Stadt" } },
        { "town learning main 1", new string [] { "Let's learn the layout of the town!\nIn this phase, you'll be asked to locate stores in town, one by one.", "Lassen Sie uns die räumliche Anordnung der Stadt kennenlernen!\nIn dieser Phase werden Sie darum gebeten, ein Geschäft nach dem anderen ausfindig zu machen." } },
        { "town learning main 2", new string [] { "Let's do it again!\nPlease locate all of the stores one by one.", "Lassen Sie uns das noch einmal machen!\nBitte finden Sie ein Geschäft nach dem anderen." } },

        { "town learning prompt 1", new string[] { "Next target store is the ", "Nächstes Ziel: " } },
        { "town learning prompt 2", new string[] { "Please navigate to the ", "Nächstes Ziel: " } },

        { "two btn er left button correct message", new string [] { " Press the <i>left button</i> \nfor correct recall",
                                                                    "Drücken Sie die <i>nach links-Taste</i> \n für richtige Erinnerung" } },
        { "two btn er left button incorrect message", new string [] { " Press the <i>left button</i> \nfor incorrect recall",
                                                                      "Drücken Sie die <i>nach links-Taste </i> \n für falsche Erinnerung" } },
        { "two btn er right button correct message", new string [] { "Press the <i>right button</i>\nfor correct recall",
                                                                     "Drücken Sie die <i>nach rechts-Taste</i> \n für richtige Erinnerung" } },
        { "two btn er right button incorrect message", new string [] { "Press the <i>right button</i>\nfor incorrect recall",
                                                                       "Drücken Sie die <i>nach rechts-Taste</i> \n für falsche Erinnerung" } },

        { "two btn er keypress practice left button correct message", new string [] { " Press the <b><i>left button</i></b> \nfor correct recall",
                                                                                      "Drücken Sie die <b><i>nach links-Taste</i></b> \nfür richtige Erinnerung" } },
        { "two btn er keypress practice left button incorrect message", new string [] { " Press the <b><i>left button</i></b> \nfor incorrect recall",
                                                                                        "Drücken Sie die <b><i>nach links-Taste</i></b> \nfür falsche Erinnerung" } },
        { "two btn er keypress practice right button correct message", new string [] { "Press the <b><i>right button</i></b>\nfor correct recall",
                                                                                       "Drücken Sie die <b><i>nach rechts-Taste</i></b> \nfür richtige Erinnerung" } },
        { "two btn er keypress practice right button incorrect message", new string [] { "Press the <b><i>right button</i></b>\nfor incorrect recall",
                                                                                         "Drücken Sie die <b><i>nach rechts-Taste</i></b> \nfür falsche Erinnerung" } },

        { "practice invitation", new string [] { "Let's practice!", "Übungsrunde" } },
        { "practice hospital", new string [] { "Great, now you are familiar with the stores in the city.\nNow, let's start delivering items!", "Jetzt sind Sie mit den Geschäften der Stadt vertraut. \nFangen wir damit an, Gegenstände auszuliefern!" } },

        { "one btn er check main", new string [] { "Let's make sure your keys are working.", "Lassen Sie uns sichergehen, dass Ihre Tasten korrekt funktionieren." } },

        { "two btn er check description left button", new string [] { "Please press the <i><b>left button</b></i>, and make sure the text on the left is bolded:",
                                                                      "Bitte drücken Sie die <i><b>nach links-Taste</b></i> und kontrollieren Sie, dass der Text links in fetter Schrift dargestellt wird." } },
        { "two btn er check description right button", new string [] { "Please press the <i><b>right button</b></i>, and make sure the text on the right is bolded:",
                                                                       "Bitte drücken Sie die <i><b>nach rechts-Taste</b></i> und kontrollieren Sie, dass der Text rechts in fetter Schrift dargestellt wird." } },
        { "two btn er check try again main", new string [] { "Try again!", "Versuchen Sie es noch einmal!" } },
        { "two btn er check try again description", new string [] { "Make sure you press the designated buttons after saying each word.", "Stellen Sie sicher, dass Sie die vorgesehenen Knöpfe drücken, nachdem Sie einen Gegenstand genannt haben." } },

        { "two btn er keypress practice main", new string [] { "Let's practice pressing the keys.", "Üben wir, die richtigen Knöpfe zu drücken." } },
        { "two btn er keypress practice description", new string [] { "When the <b>right button</b> text becomes bolded - press the\nright button\n\n" +
                                                                      "When the <b>left button</b> text becomes bolded - press the\nleft button",
                                                                      "Wenn die Worte <b>nach-rechts Taste</b> fett dargestellt werden - drücken Sie die <b>nach rechts-Taste</b>" + "Wenn die Worte <b>nach links-Taste</b> fett dargestellt werden - drücken Sie die <b>nach links-Taste </b>" }},

        { "one btn efr instructions title", new string[] { "Externalized Free Recall (EFR) Instructions" , "Erweiterter Freier Gedächtnisabruf (EFR)"} },
        { "one btn efr instructions main", new string[] { "In this section of the study, we would like you to vocalize words that come to your mind during the free recall sections (the long sections directly following deliveries, NOT the recalls with store cues).\n\nPlease continue to recall as many words as possible from the just-presented list. In addition, every time a specific, salient word comes to mind, say it aloud, even if you have already recalled it or if it was not presented on the most recent delivery day.\n\nOnly say other words if they come to mind as you are trying to recall items on the most recently presented list. This is not a free-association task.\n\nIf the word you have just said aloud was NOT presented on the most recent list, or if you have already recalled it in this recall period, press the B key after recalling that word, but before recalling the next word.",
                                                      "In diesem Teil des Experiments möchten wir Sie bitten, Wörter auszusprechen, die Ihnen während der freien Erinnerungsabschnitte in den Sinn kommen (d.h., in den langen Abschnitten direkt nach den Lieferungen, NICHT in den Abrufphasen, in denen jeweils ein Geschäft gezeigt wird).\n\nBitte nennen Sie weiterhin so viele Gegenstände des vorausgehenden Liefertags wie möglich. Darüber hinaus nennen Sie jetzt Gegenstände, die Ihnen einfallen, aber auch dann, auch wenn Sie sie bereits zuvor genannt haben oder sie am letzten Liefertag nicht geliefert wurden.\n\n Nennen Sie andere Wörter nur dann, wenn sie Ihnen in den Sinn kommen, während Sie versuchen, sich an die am vorausgehenden Liefertag gelieferten Gegenstände zu erinnern. (Dies ist keine freie Assoziationsaufgabe.)\n\nFalls das Wort, das Sie gerade ausgesprochen haben, Ihrer Meinung nach NICHT zu den Gegenständen des letzten Liefertags gehört oder wenn Sie es in dieser Erinnerungsperiode bereits zuvor abgerufen haben, drücken Sie die (B)-Taste, nachdem Sie dieses Wort ausgesprochen haben, aber bevor Sie das nächste Wort aussprechen."} },
        { "one btn efr instructions main no reject", new string[] { "In this section of the study, we would like you to vocalize words that come to your mind during the free recall sections (the long sections directly following deliveries, NOT the recalls with store cues).\n\nPlease continue to recall as many words as possible from the just-presented list. In addition, every time a specific, salient word comes to mind, say it aloud, even if you have already recalled it or if it was not presented on the most recent delivery day.\n\nOnly say other words if they come to mind as you are trying to recall items on the most recently presented list. This is not a free-association task.",
                                                      "In diesem Teil des Experiments möchten wir Sie bitten, Wörter auszusprechen, die Ihnen während der freien Erinnerungsabschnitte in den Sinn kommen (d.h., in den langen Abschnitten direkt nach den Lieferungen, NICHT in den Abrufphasen, in denen jeweils ein Geschäft gezeigt wird).\n\nBitte nennen Sie weiterhin so viele Gegenstände des vorausgehenden Liefertags wie möglich. Darüber hinaus nennen Sie jetzt Gegenstände, die Ihnen einfallen, aber auch dann, auch wenn Sie sie bereits zuvor genannt haben oder sie am letzten Liefertag nicht geliefert wurden.\n\n Nennen Sie andere Wörter nur dann, wenn sie Ihnen in den Sinn kommen, während Sie versuchen, sich an die am vorausgehenden Liefertag gelieferten Gegenstände zu erinnern. (Dies ist keine freie Assoziationsaufgabe.)"} },

        { "one btn ecr instructions title", new string[] { "Required Response Cued Recall (RCR) Instructions" , "Abruf von Gegenständen nach Anzeige des Geschäfts" } },
        { "one btn ecr instructions main", new string[] { "Next, stores will be presented on the screen one at a time. For each store, please try and recall, aloud, the item you delivered to it.\n\nWe are interested in your best guess as to the delivered item, so even if you are not sure if the remembered item is correct, please say it aloud into the microphone and press the ‘B’ button after saying the item to indicate that the item you recalled was a guess. \n\nYou’ll do this for all stores visited in the delivery day.",
                                                      "Als nächstes werden nacheinander Bilder von Geschäften auf dem Bildschirm angezeigt. Bitte versuchen Sie, für jedes Geschäft den Gegenstand zu nennen, den Sie dorthin geliefert haben.\n\n Wir sind an Ihrer besten Einschätzung des gelieferten Gegenstands interessiert; nennen Sie den Gegenstand bitte selbst dann, wenn Sie sich nicht sicher sind, ob Ihre Erinnerung korrekt ist; sprechen seinen Namen in das Mikrofon und drücken anschließend die (B)-Taste, um anzuzeigen, dass es sich bei dem gerade genannten Gegenstand um eine Vermutung handelte. \n\nTun Sie dies bitte für alle Geschäfte, die am jeweiligen Liefertag besucht worden sind."} },

        { "one btn er keypress practice main", new string [] { "Let's practice pressing the reject key.", "Lassen Sie uns üben, die Taste zur Kennzeichnung unsicherer/falscher Abrufe zu drücken." } },
        { "one btn er keypress practice description", new string [] { "Press the (B) key 20 times and wait about 3 seconds between each keypress.\n\nThe screen will automatically continue when you are done.",
                                                                   "Drücken Sie die Taste (B) 20 Mal und warten Sie zwischen den Tastendrücken etwa 3 Sekunden.\n\nDie Bildschirmanzeige wird automatisch aktualisiert, wenn Sie mit dieser Aufgabe fertig sind." } },

        { "er check main", new string[] { "Let's make sure your keys are working.", "Wir testen nun die Tastenzuordnung."} },
        { "er check pass", new string[] { "Great! Your keys are working.", "Sehr gut! Die Tasten funktionieren."} },

        { "er check understanding title", new string[] { "ER Review", "" } },
        { "er check understanding main", new string[] { "Please press the buzzer to call the researcher in now.", "Bitte rufen Sie nun den Versuchsleiter."} },
        { "er check understanding main hospital", new string[] { "Please call the researcher now.", "Bitte rufen Sie jetzt den Versuchsleiter." } },

        { "navigation note title", new string[] { "Quick Note!", "Hinweis" } },
        { "navigation note main", new string[] { "Please navigate from store to store as quickly and efficiently as you can.", "Bitte versuchen Sie, möglichst schnell und auf dem kürzesten Weg zum Zielgeschäft zu fahren." } },

        { "classifier delay note title", new string[] { "Quick Note!", "Hinweis" } },
        { "classifier delay note main", new string[] { "For the remaining sessions, you may notice a slight lag once you arrive at each store. Please note that this is purposeful and not an error in the experiment.", "In den verbleibenden Runden stellen Sie möglicherweise eine leichte Verzögerung fest, sobald Sie an Geschäften ankommen. Dies ist kein Fehler, sondern beabsichtigt." } },

        { "fixation item", new string [] {"+", "+"} },
        { "fixation practice message", new string [] {"Please look at the plus sign", "Bitte schauen Sie auf das Pluszeichen"} },

        { "fr item", new string [] { "*******", "*******" } },
        { "speak now", new string [] { "(Please speak now)", "(Bitte sprechen Sie jetzt)"} },
        { "one btn er message", new string [] { "Press the (B) key to reject a recalled item", "Drücken Sie die Taste (B), wenn ein erinnerter Gegenstand eine Vermutung war" } },
        { "one btn er message store", new string [] { "Press the (B) key to reject a recalled store", "Drücken Sie die Taste (B), wenn ein erinnertes Geschäft eine Vermutung war" } },

        { "free recall title", new string [] { "Free Recall", "Freier Abruf"} },
        { "free recall main", new string [] { "Try to recall all the items that you delivered to the stores in this delivery day.\n\nType one item and press <Enter> to submit and type your next response", 
                                              "Versuchen Sie bitte, sich an alle Gegenstände zu erinnern, die Sie an diesem Liefertag zu den Geschäften geliefert haben.\n\nGeben Sie den Namen eines Gegenstands ein und drücken Sie die Eingabetaste, um ihn zu speichern. Geben Sie dann Ihre nächste Antwort ein u.s.w."}},
        { "value guess title", new string [] { "Value Guess", GERMAN_TRANSLATION_NEEDED} },
        { "value guess main", new string [] { "Please try to estimate the average offer you received during the delivery day.", GERMAN_TRANSLATION_NEEDED} },
        { "cued recall message", new string [] { "Press the (X) key after recalling the item to move to the next store", 
                                                 "Drücken Sie (X), nachdem Sie den Namen des gelieferten Gegenstands ausgesprochen haben, um zum nächsten Geschäft zu gelangen." } },
        { "cued recall title", new string [] { "Cued Recall", "Abruf des zu einem Geschäft gelieferten Gegenstands"} },
        { "online cued recall main", new string [] { "Please recall which item you delivered to the store shown on the screen.\n\nPress the Enter key after recalling the item to move to the next store", 
                                                     "Bitte erinnern Sie sich, welchen Gegenstand Sie zu dem auf dem Bildschirm angezeigten Geschäft geliefert haben.\n\nDrücken Sie die Eingabetaste, nachdem Sie den Namen des Gegenstands eingegeben haben, um zum nächsten Geschäft zu gelangen."} },
        { "one btn ecr message", new string[] { "Press the (B) key if a recalled item was a guess", "Drücken Sie die Taste (B), wenn ein erinnerter Gegenstand eine Vermutung war" } },


        { "deliv day pointing accuracy title", new string[] {"Pointing Accuracy", "Zeigegenauigkeit"} },
        { "deliv day progress title", new string[] {"", ""} },


        { "final store recall title", new string [] {"Final Store Recall", "Abschließender Abruf der Geschäfte"} },
        { "final store recall main", new string [] {"Try to recall stores that you delivered items to.\n\nNote that you need to recall the store names", "Versuchen Sie bitte, sich an die Geschäfte zu erinnern, denen Sie Gegenstände geliefert haben.\n\nBitte beachten Sie, dass Sie sich hier an die Namen der <i>Geschäfte</i> erinnern sollen"} },
        { "final store recall text", new string [] {"Start typing store name one at a time...", "Bitte geben Sie die Namen der Geschäfte nacheinander ein..."}},
        
        { "final object recall title", new string [] {"Final Item Recall", "Abschließender Abruf der gelieferten Gegenstände"} },
        { "final object recall main", new string [] {"Try to recall all of the items that you delivered so far across all delivery days.", "Versuchen Sie bitte, sich an alle Gegenstände zu erinnern, die Sie bisher an allen Auslieferungstagen geliefert haben."} },
        { "final object recall text", new string [] {"Start typing item one at a time...", "Bitte geben Sie die Namen der Gegenstände nacheinander ein..."}},

        { "play movie", new string[] {"Press any key to play movie.", "Starten Sie das Video mit beliebiger Taste."} },
        { "recording confirmation", new string[] {"Did you hear the recording? \n(Y = Continue / N = Try Again / C = Cancel).",
                                                  "War die Aufnahme verständlich? \n(Y = Ja, weiter / N = Neuer Versuch / C = Abbrechen)."} },
        { "playing", new string[] {"Playing...", "Wiedergabe läuft…"} },
        { "recording", new string[] {"Recording...", "Aufnahme läuft…"} },
        { "after the beep", new string[] {"Press any key to record a sound after the beep.", "Drücken Sie eine beliebige Taste, um nach dem Piepton eine Testaufnahme zu starten."} },
        { "running participant", new string[] {"Running a new session of Delivery Person. \n Press (Y) to continue, (N) to quit.",
                                               "Start von Fahrradkurier.\n Drücken Sie (Y) um fortzufahren, (N) um abzubrechen.",} },
        { "begin session", new string[] {"Begin session", "Start einer neuen Runde"} },
        { "break", new string[] {"It's time for a short break.\nPlease wait for the researcher\nto come check on you before continuing the experiment.\n\nResearcher: Press space to resume the experiment.",
                                 "Zeit für eine kurze Pause.\nBitte warten Sie auf die Fortsetzung des Experiments durch den Versuchsleiter.\n\nVersuchsleiter: Drücken Sie die Leertaste, um das Experiment wieder aufzunehmen."} },

        { "music video instructions title", new string[] { "Music Video Instructions", "Anleitung Musikvideo" } },
        { "music video instructions main", new string[] { "You will now be asked to watch a series of music videos. After each individual video, you will rate how familiar you are with the just presented video and how engaged you were during the presentation of the video. Use the Left Trigger and Right Trigger (big buttons on the back of the controller) to move the rating sliders. \n\nWe encourage you to engage with these videos as you would if you were watching them for leisure. \n\nOver the course of the study, the videos will repeat. Please do your best to avoid listening to the presented songs or watching the videos outside of the study. \n\nPlease let the experimenter know if you have any questions; then continue to the first video.",
                                                          "Im Anschluss sehen sie nun eine Reihe von Musikvideos. Nach jedem einzelnen Video bewerten Sie, wie vertraut Sie mit dem gerade präsentierten Video sind und wie stark Sie die Präsentation des Videos angesprochen hat. Verwenden Sie den linken Trigger und den rechten Trigger des Game-Controllers, um die Bewertungen einzustellen. \n\nBitte sehen Sie sich diese Videos so an, wie Sie das in Ihrer Freizeit tun würden. \n\nIm Laufe der Studie werden die Videos wiederholt. Bitte tun Sie Ihr Bestes, zu vermeiden, die präsentierten Lieder außerhalb der Studie zu hören oder die Videos außerhalb der Studie anzusehen. \n\nBitte lassen Sie den Experimentator wissen, wenn Sie Fragen haben; fahren Sie dann mit dem ersten Video fort." } },
        { "music video familiarity title", new string[] { "On a scale of very unfamiliar to very familiar, please rate your familiarity with this video.", "Bitte bewerten Sie auf einer Skala von sehr wenig vertraut bis sehr vertraut Ihre Vertrautheit mit diesem Video." } },
        { "music video familiarity rating 0", new string[] { "very unfamiliar", "sehr wenig vertraut"} },
        { "music video familiarity rating 1", new string[] { "somewhat unfamiliar", "wenig vertraut" } },
        { "music video familiarity rating 2", new string[] { "neutral", "neutral" } },
        { "music video familiarity rating 3", new string[] { "somewhat familiar", "etwas vertraut" } },
        { "music video familiarity rating 4", new string[] { "very familiar", " sehr vertraut" } },
        { "music video engagement title", new string[] { "On a scale of very disengaged to very engaged, please rate how engaged you were during the presentation of this video.", "Bitte bewerten Sie auf einer Skala von <i>sehr wenig angesprochen</i> bis <i>sehr stark angesprochen</i>, wie stark Sie die Präsentation dieses Videos angesprochen hat." } },
        { "music video engagement rating 0", new string[] { "very disengaged", "sehr wenig angesprochen" } },
        { "music video engagement rating 1", new string[] { "somewhat disengaged", "wenig angesprochen" } },
        { "music video engagement rating 2", new string[] { "neutral", "neutral" } },
        { "music video engagement rating 3", new string[] { "somewhat engaged", "stark angesprochen" } },
        { "music video engagement rating 4", new string[] { "very engaged", "sehr stark angesprochen" } },
        { "music video recall instructions title", new string[] { "Music Video Recall Instructions", "Erinnerungsphase zu den Musikvideos" } },
        { "music video recall instructions main", new string[] { "Over the next ~10 minutes, you will be prompted to recall as much as you can from each music video that you have been shown during this session (3 minutes per video). \n\nOne at a time, the title of the song and a screencap from the video will appear on screen for 5 seconds. \n\nOnce these disappear from the screen, you can begin recalling as much as you remember from the music video. During this period, only recall information from the song/music video that was prompted. Be as descriptive as possible. \n\nPlease be aware that there is no right or wrong way to do this, and we encourage you to say whatever comes to mind when thinking back to these videos. \n\nPlease let the experimenter know if you have any questions; then continue to the first video recall.",
                                                    "Im nächsten Teil des Experiments erinnern Sie sich bitte nacheinander an möglichst vieles in jedem der 3 Videos, die Ihnen in dieser Sitzung gezeigt wurden. \n\nZunächst werden der Titel und eine Szene aus jedem Video 5 Sekunden lang auf dem Bildschirm angezeigt. \n\nSobald diese Informationen ausgeblendet werden, beschreiben Sie bitte über einen Zeitraum von 3 Minuten möglichst viele Details des Videos. Rufen Sie während dieser Zeit nur Informationen aus dem Lied/Musikvideo ab, dessen Titel direkt zuvor angezeigt wurde. \n\nBeachten Sie bitte, dass es dabei keinen richtigen oder falschen Weg gibt; sagen Sie bitte alles, was Ihnen in den Sinn kommt, wenn Sie an das jeweilige Video zurückdenken. \n\nBitte lassen Sie den Versuchsleiter wissen, wenn Sie Fragen haben." } },
        { "music video ending instructions", new string[] {"\nPress (Y) to continue to the questions.", "\nDrücken Sie (Y), um mit den Fragen fortzufahren." } },

        { "music video question 0 title", new string[] { "Do you have a musical background?", "Haben Sie einen musikalischen Hintergrund?" } },
        { "music video question 0 rating 0", new string[] { "no", "nein" } },
        { "music video question 0 rating 1", new string[] { "yes", "ja" } },

        { "please find prompt", new string[] {"please find the ", "Ziel: "} },
        { "bakery", new string[] {"bakery", "Bäckerei"} },
        { "barber shop", new string[] {"barber shop", "Friseur"} },
        { "bike shop", new string[] {"bike shop", "Fahrradladen"} },
        { "cafe", new string[] {"cafe", "Cafe"} },
        { "clothing store", new string[] {"clothing store", "Kleidungsgeschäft"} },
        { "dentist", new string[] {"dentist", "Zahnarzt"} },
        { "craft shop", new string[] {"craft shop", "Bastelladen"} },
        { "grocery store", new string[] {"grocery store", "Supermarkt"} },
        { "jewelry store", new string[] {"jewelry store", "Juwelier"} },
        { "florist", new string[] {"florist", "Blumenladen"} },
        { "hardware store", new string[] {"hardware store", "Baumarkt"} },
        { "gym", new string[] {"gym", "Fitnessstudio"} },
        { "pizzeria", new string[] {"pizzeria", "Pizzeria"} },
        { "pet store", new string[] {"pet store", "Tierhandlung"} },
        { "music store", new string[] {"music store", "Musikgeschäft"} },
        { "pharmacy", new string[] {"pharmacy", "Apotheke"} },
        { "toy store", new string[] {"toy store", " Spielwarenladen"} }, 

        // { "the bakery", new string[] {"bakery", "die Bäckerei"} },
        // { "the barber shop", new string[] {"barber shop", "den Friseur"} },
        // { "the bike shop", new string[] {"bike shop", "den Fahrradladen"} },
        // { "the cafe", new string[] {"cafe", "das Cafe"} },
        // { "the clothing store", new string[] {"clothing store", "das Kleidungsgeschäft"} },
        // { "the dentist", new string[] {"dentist", "den Zahnarzt"} },
        // { "the craft shop", new string[] {"craft shop", "den Bastelladen"} },
        // { "the grocery store", new string[] {"grocery store", "den Supermarkt"} },
        // { "the jewelry store", new string[] {"jewelry store", "den Juwelier"} },
        // { "the florist", new string[] {"florist", "den Blumenladen"} },
        // { "the hardware store", new string[] {"hardware store", "den Baumarkt"} },
        // { "the gym", new string[] {"gym", "das Fitnessstudio"} },
        // { "the pizzeria", new string[] {"pizzeria", "die Pizzeria"} },
        // { "the pet store", new string[] {"pet store", "die Tierhandlung"} },
        // { "the music store", new string[] {"music store", "das Musikgeschäft"} },
        // { "the pharmacy", new string[] {"pharmacy", "die Apotheke"} },
        // { "the toy store", new string[] {"toy store", "den Spielwarenladen"} }, 


        { "confetti", new string[] {"confetti", "Konfetti"} },
    };

    private static Dictionary<string, string[]> language_formattable_string_dict = new Dictionary<string, string[]>()
    {
        {"", new string[] {"", ""}},

        {"deliv day pointing accuracy main", new string[] { "Good job! \n\nYou correctly pointed to {0} out of {1} stores in this delivery day.", "Gut gemacht! \n\nSie haben an diesem Liefertag korrekt auf {0} von {1} Geschäften an diesem Auslieferungstag gezeigt."} },
        {"deliv day progress main", new string[] { "{0} out of {1} delivery days completed!!", "{0} von {1} Auslieferungstagen abgeschlossen!"} },
    };

    public static string GetLanguageString(string string_name)
    {
        if (!language_string_dict.ContainsKey(string_name))
            throw new UnityException("I don't have a language string called: " + string_name);
        return language_string_dict[string_name][(int)current_language];
    }

    public static string GetFormattableLanguageString(string string_name, string[] format_values)
    {
        if (!language_formattable_string_dict.ContainsKey(string_name))
            throw new UnityException("I don't have a language string called: " + string_name);
        return string.Format(language_formattable_string_dict[string_name][(int)current_language], format_values);
    }
}
