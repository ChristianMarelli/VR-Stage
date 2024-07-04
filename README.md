# VR-stage nuova versione
 
Progetto vr stage


SPEECH-TO-TEXT LOG-IN
All'interno della Start scene è presente il Game Object "App Voice Experience" che si occupa dell'ascolto.
La trascrizione all'interno dei campi di testo per il log-in è gestita nel file "LogHandler", presente nella scena come comnponente di "App Voice Experience".

SPEECH-TO-TEXT CHAT
All'interno della GenericRoom è presente il Game Object "App Voice Experience" che si occupa dell'ascolto.
La trascrizione all'interno del campo di testo della chat è gestita nel file "ChatHandler", presente nella scena come comnponente di "App Voice Experience".

PLACEMARK
Prefab bandierina ---> Assets/Resources/Bandierina.prefab
Lo script per l'apparizione delle bandierine è  Object_Spawner, presente come componente del gameObject "RightHand Controller"

MEASUREMENTS
Prefab metro  ---> Assets/Resources/metro.prefab
Prefab misura ---> Assets/Resources/testo.prefab
Lo script per la misurazione è  MeasureRPC, presente come componente del gameObject "XR Origin"

MENU' FISSO
Prefab ---> Assets/Resources/ImpostazioniFisse.prefab

MENU' A COMPARSA
Prefab ---> Assets/Resources/Impostazioni.prefab
Lo script per l'apertura e la chiusura menù è MenuSpawn, presente come componente del gameObject "LeftHand Controller"

AVATAR PROFESSORE 
Prefab ---> Assets/Resources/Player.prefab

AVATAR STUDENTE
Prefab ---> Assets/Resources/PlayerStudent.prefab

XR-RAY Interactor
Il gameObject che rappresenta il raggio è "RayInteractorD" assegnato al prefab "Player"
Lo script per l'attivazione e la disattivazione del raggio è "RPCRaggio", presente come componente  del prefab "Player"

NOTIFICA MESSAGGI 
Lo script per la comparsa dell'icona di notifica è "ChatManager", presente come componente del gameObject "Chat"
