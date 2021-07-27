# PunchAR

# Título.
PunchAR

# Descripción.
Adaptar el juego PunchOut!! a la realidad aumentada.

PunchAR te permite luchar en cualquier lugar trayendo a tus personajes a la realidad.

Controlarás a tu personaje haciendo uso de botones de realidad aumentada o botones en la pantalla de tu dispositivo.

# Plataformas del proyecto.
Android.

# Objetivos de la aplicación.
Entretenimiento.

# Justificación.
Aplicar los conocimientos adquiridos durante el curso y jugar PunchOut.

# Mecánicas del proyecto.
En la pantalla inicial el usuario puede salir del juego o seleccionar un modo de juego: con botones AR o con botones en pantalla. Luego se le pide que coloque las marcas necesarias de forma visible y coloque a los personajes a una distancia correcta, hecho esto el combate comienza automaticamente. Cada encuentro consiste de 3 rounds, el usuario se enfrenta a 3 adversarios (CPU), cada uno con una dificultad ascendente. Si el usuario pierde un encuentro debe volver al menu y empezar desde el primer encuentro, si gana los 3 encuentros, se le muestra una pantalla de victoria. 

# Ambiente donde se desarrolló.
Unity2019.4.28f1 - Vuforia9.8.8

# Marcas utilizadas.
![MarcasAR](https://user-images.githubusercontent.com/35788695/127080949-2fd6767d-8d64-42a7-8aa8-d9c4e659fd18.jpg)

Las marcas de botones se usan para el modo de juego con botones AR, cuando la marca deja de ser detectada se considera que el boton ha sido precionado y se realiza la accion correspondiente. Boton Rojo: golpe izquierdo, Boton Azul: golpe derecho, Boton Verde: esquiva izquierda, Boton Amarillo: esquiza derecha.

# Posibles problemas que se encontraron durante el desarrollo.
Lo que genero mas bugs durante el desarrollo fue el uso de corutinas, y la comunicacion entre el jugador y el enemigo. Las corutinas se emplearon debido a que existen varios eventos que necesitan manejar tiempo real y esperar una cantidad determinada de segundos antes de seguir su ejecucion. La comunicacion entre jugador y enemigo es crucial, ya que los boxeadores no usan colliders, entonces las acciones de golpear y esquivar se manejan pasandose mensajes entre ellos.

# Posibles cambios en el planeamiento inicial.
Originalmente se habia planteado solo tener el modo de juego con los botones AR, pero por recomendacion se decidio implementar tambien el modo con botones de unity.

# Conclusión y Recomendaciones a futuro.

La Realidad Aumentada puede ofrecer nuevas formas de implementar juegos clasicos, trayendo nuevos retos a los desarrolladores dada sus limitaciones y experiencias nuevas a los jugadores.  
Recomendamos implementar los cambios necesarios para poder jugar con un dispositivo de Realidad Virtual.
