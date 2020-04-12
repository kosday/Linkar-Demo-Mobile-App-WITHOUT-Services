# Proyecto App Movil Sin Servicios

Las llamadas se hacen usando conexiones directas a Linkar Server. El dispositivo móvil se conectará directamente a Linkar Server sin necesidad de usar un Servicio Web.

LinkarViewFiles (Shared Project)

LinkarViewFiles.Android.

LinkarViewFiles.IOS.

Los 3 proyectos se crean automáticamente al elegir la plantilla de Cross-Platform Aplicación móvil (Xamarin.Forms). Dentro de esta plantilla seleccionaremos las plataformas a las que va dirigido (Android e IOS) y la Estrategia de uso compartido de código (Proyecto compartido). Esta demo usa como base la plantilla Master-Detail.

En la carpeta \LinkarViewFiles podemos encontrar una Solución Visual Studio con 3 proyectos:

LinkarViewFiles (Proyecto compartido)
Mas información sobre como crear este tipo de proyectos aquí.

LinkarViewFiles.Android.
Mas información sobre este tipo de proyectos aquí.

LinkarViewFiles.IOS.
Mas información sobre este tipo de proyectos aquí. 

- Login

Los datos de acceso en este ejemplo no son capturados en la pantalla, en este caso se han establecido internamente. Y únicamente usamos esta pagina para generar un token contra el servicio web que nos valide para el resto de llamadas

- About Demo

Página resumen de la funcionalidad del resto de páginas.

- Customers

La lista trae todos los Customers de la base de datos, el detalle nos muestra cada una de ellas cuando pulsamos sobre la lista.

- Items

La lista trae todos los Items de la base de datos, el detalle nos muestra cada una de ellas cuando pulsamos sobre la lista.

- Orders

La lista trae todas las Orders de la base de datos, el detalle nos muestra cada una de ellas cuando pulsamos sobre la lista. Podemos navegar por los multivalores y subvalores al seleccionarlos.
