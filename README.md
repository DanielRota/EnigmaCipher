Daniel Rota 5IC 2022/2023 Sistemi e Reti
CIFRARIO ENIGMA
INTRODUZIONE

Il Cifrario Enigma risale ai tempi della Seconda Guerra Mondiale, durante la quale i tedeschi necessitavano di scambiarsi informazioni segrete, essenziali ai fini del conflitto, facendo in modo che queste non finissero nelle mani dell'Intesa. La cifratura di suddetti messaggi, ossia la codifica in un testo completamente irriconoscibile senza l'utilizzo di strumenti di decifratura, risponde a questa necessità.

La tecnica di cifratura rappresenta un'evoluzione dell'antico Cifrario di Cesare, facente parte dei cifrari a sostituzione monoalfabetica.
STRUTTURA

L'applicativo sviluppato è una rappresentazione della versione "Rocket" (1941) della macchina originale; vi troviamo tre Rotori, dischi riportanti le ventisei lettere dell'alfabeto, disposte normalmente su una faccia, e in un ordine diverso sull'altra, un Riflettore, componente in grado di "Riflettere" due lettere facendo riferimento ad una specifica configurazione, e infine una "Plugboard" o "Invertitore", un pannello sul quale l'utente ha la possibilità di invertire di posizione due lettere qualsiasi all'interno del tradizionale alfabeto.
SPECIFICHE

I componenti meccanici del dispositivo appena introdotti, vengono rappresentati all'interno del software da molteplici Classi, troviamo:

    Rotor: Presenta un Offset, il numero totale di giri che il Rotore ha compiuto durante lo svolgersi delle attività, e una Configuration, ovvero una specifica permutazione dell'alfabeto, alla quale il componente si appoggia per eseguire la propria operazione di cifratura, il cui output diviene l'input del componente successivo.
    Reflector: Classe base di Rotor, a cui estende il campo Configuration e vari altri metodi, facendo uso dei meccanismi di Ereditarietà e Polimorfismo, si occupa di sostituire il carattere in input con il suo corrispettivo della propria configurazione.
    Plugboard: A seconda della scelta dell'utente, inverte due caratteri all'interno del normale alfabeto; viene utilizzato nella parte iniziale del processo di crittatura.

PROCEDURA ED ESEMPIO

Consideriamo una Plugboard, tre Rotori e un Riflettore, disposti esattamente in questo ordine:

"ABCDEFGHIJKLMNOPQRSTUVWXYZ"
"JGDQOXUSCAMIFRVTPNEWKBLZYH"
"NTZPSFBOKMWRCJDIVLAEYUXHGQ"
"JVIUBHTCDYAKEQZPOSGXNRMWFL"
"QYHOGNECVPUZTFDJAXWMKISRBL"

Supponiamo che l'utente abbia selezionato sulla Plugboard i caratteri "A" ed "K", che vengono quindi scambiati di posto, ottenendo la configurazione:

"KBCDEFGHIJALMNOPQRSTUVWXYZ"

Ipotizzando di voler cifrare il carattere "A", questo deve essere inizialmente confrontato con la configurazione dell'Invertitore: il carattere "A" avente index zero, viene sostituito con la lettera "K", anch'essa con lo stesso indice a seguito dello scambio.

Di seguito il primo Rotore, che condivide pressoché il medesimo procedimento appena visto; la lettera "K" si trova all'undicesima posizione nella configurazione del Rotore appena preso in esame, viene quindi selezionato l'undicesimo carattere all'interno del normale alfabeto, "M".

La lettera avente index tredici, ci porta quindi a scegliere la tredicesima lettera dell'alfabeto, "C". Svolgendo lo stesso procedimento, otteniamo il carattere "I" come output dal terzo Rotore.

Viene poi il Riflettore, componente adibito all'associazione di due specifici caratteri, confrontando infatti configurazione ed alfabeto, troviamo ad esempio le coppie "QA"-"AQ", "YB"-"BY", e così discorrendo. Viene quindi naturale ipotizzare che la lettera entrante, venga sostiuita con la sua corrispettiva; "I" diviene effettivamente "V".

Dopo aver eseguito il primo giro di cifratura, il carattere viene modificato nuovamente passando tuttavia inizialmente il terzo Rotore, poi per il secondo, per il primo, e infine per l'Invertitore, ovviamente viene anche alterato l'algoritmo di cifratura; invece di fare affidamento alla configurazione del componente per ottenere l'indice del carattere in output sull'alfabeto, avviene il procedimento contrario.

Seguendo le operazioni sopra descritte, il carattere "V" viene codificato con la lettera "B", poi con "G", successivamente "B" e infine "B".

Dopo la configurazione di ogni carattere, la configurazione del Rotore I slitta di una posizione in avanti, divenendo:

"HJGDQOXUSCAMIFRVTPNEWKBLZY"

Dopo aver compiuto ventisei cifrature, ovvero un giro completo dell'alfabeto, la configurazione del Rotore II viene spostata in avanti di una posizione. La stessa logica viene applicata al Rotore III.

Questa meccanica rappresenta il fulcro principale della macchina enigma, in quanto permette ad un singolo carattere di essere cifrato ogni volta con una lettera diversa, aumentando quindi la complessità del messaggio cifrato e la difficoltà nel decriptarlo.

Di seguito una rappresentazione grafica di quanto spiegato:

Image
