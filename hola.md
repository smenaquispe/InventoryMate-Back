Usuarios (Users):

id (Primary Key)
nombre de usuario
contraseña (hash)
correo electrónico
rol (administrador, empleado, etc.)


Productos (Products):

id (Primary Key)
nombre del producto
descripción
precio
cantidad disponible
proveedor
fecha de creación
fecha de última actualización

Transacciones (Transactions):

id (Primary Key)
tipo (entrada, salida, ajuste)
id del producto (Foreign Key a la tabla Products)
cantidad
fecha y hora de la transacción
usuario responsable (Foreign Key a la tabla Users)

Ventas (Sales):

id (Primary Key)
id del producto (Foreign Key a la tabla Products)
cantidad
precio de venta
fecha y hora de la venta
usuario responsable (Foreign Key a la tabla Users)

Notificaciones (Notifications):

id (Primary Key)
id del usuario (Foreign Key a la tabla Users)
tipo (bajo inventario, producto vencido, etc.)
mensaje
fecha y hora de la notificación