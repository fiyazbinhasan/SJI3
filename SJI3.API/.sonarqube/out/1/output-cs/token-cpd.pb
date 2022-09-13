ﬁ

^C:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\AntiCorruption\Domain\TaskProcessingService.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
AntiCorruption ,
., -
Domain- 3
;3 4
public		 
class		 !
TaskProcessingService		 "
:		# $"
ITaskProcessingService		% ;
{

 
private 
readonly 

IAppLogger 
<  !
TaskProcessingService  5
>5 6
_logger7 >
;> ?
public 
!
TaskProcessingService  
(  !

IAppLogger! +
<+ ,!
TaskProcessingService, A
>A B
loggerC I
)I J
{ 
_logger 
= 
logger 
; 
} 
public 

Task 
ProcessTask 
( 
Guid  
taskId! '
,' (
CancellationToken) :
stoppingToken; H
)H I
{ 
_logger 
. 
LogInformation 
( 
$str 8
,8 9
taskId: @
)@ A
;A B
return 
Task 
. 
CompletedTask !
;! "
} 
} Û4
fC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\AntiCorruption\HostedServices\SeedBackgroundService.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
AntiCorruption ,
., -
HostedServices- ;
;; <
public 
class !
SeedBackgroundService "
:# $
BackgroundService% 6
{ 
private 
readonly 

IAppLogger 
<  !
SeedBackgroundService  5
>5 6
_logger7 >
;> ?
private 
readonly 
IServiceProvider %
	_provider& /
;/ 0
public 
!
SeedBackgroundService  
(  !

IAppLogger! +
<+ ,!
SeedBackgroundService, A
>A B
loggerC I
,I J
IServiceProviderK [
provider\ d
)d e
{ 
_logger 
= 
logger 
; 
	_provider 
= 
provider 
; 
} 
	protected 
override 
async 
Task !
ExecuteAsync" .
(. /
CancellationToken/ @
stoppingTokenA N
)N O
{ 
_logger 
. 
LogInformation 
( 
$str ?
)? @
;@ A
await  
BackgroundProcessing "
(" #
stoppingToken# 0
)0 1
;1 2
} 
private 
async 
Task  
BackgroundProcessing +
(+ ,
CancellationToken, =
cancellationToken> O
)O P
{   
using!! 
var!! 
scope!! 
=!! 
	_provider!! #
.!!# $
CreateScope!!$ /
(!!/ 0
)!!0 1
;!!1 2
var"" 
context"" 
="" 
scope"" 
."" 
ServiceProvider"" +
.""+ ,

GetService"", 6
<""6 7
AppDbContext""7 C
>""C D
(""D E
)""E F
;""F G
if$$ 

($$ 
context$$ 
!=$$ 
null$$ 
)$$ 
{%% 	
await&& 
context&& 
.&& 
Database&& "
.&&" #
EnsureCreatedAsync&&# 5
(&&5 6
cancellationToken&&6 G
)&&G H
;&&H I
if(( 
((( 
!(( 
context(( 
.(( 
Set(( 
<(( 
ApplicationUser(( ,
>((, -
(((- .
)((. /
.((/ 0
Any((0 3
(((3 4
)((4 5
)((5 6
context)) 
.)) 
Set)) 
<)) 
ApplicationUser)) +
>))+ ,
()), -
)))- .
.)). /
Add))/ 2
())2 3
new))3 6
ApplicationUser))7 F
())F G
Guid))G K
.))K L
NewGuid))L S
())S T
)))T U
,))U V
$str))W \
,))\ ]
$str))^ h
)))h i
)))i j
;))j k
if++ 
(++ 
!++ 
context++ 
.++ 
Set++ 
<++ 
TaskUnitStatus++ +
>+++ ,
(++, -
)++- .
.++. /
Any++/ 2
(++2 3
)++3 4
)++4 5
{,, 
context-- 
.-- 
Set-- 
<-- 
TaskUnitStatus-- *
>--* +
(--+ ,
)--, -
.--- .
Add--. 1
(--1 2
new--2 5
TaskUnitStatus--6 D
(--D E
$num--E F
,--F G
nameof--H N
(--N O
TaskUnitStatus--O ]
.--] ^
TaskStatusOne--^ k
)--k l
)--l m
)--m n
;--n o
context.. 
... 
Set.. 
<.. 
TaskUnitStatus.. *
>..* +
(..+ ,
).., -
...- .
Add... 1
(..1 2
new..2 5
TaskUnitStatus..6 D
(..D E
$num..E F
,..F G
nameof..H N
(..N O
TaskUnitStatus..O ]
...] ^
TaskStatusTwo..^ k
)..k l
)..l m
)..m n
;..n o
context// 
.// 
Set// 
<// 
TaskUnitStatus// *
>//* +
(//+ ,
)//, -
.//- .
Add//. 1
(//1 2
new//2 5
TaskUnitStatus//6 D
(//D E
$num//E F
,//F G
nameof//H N
(//N O
TaskUnitStatus//O ]
.//] ^
TaskStatusThree//^ m
)//m n
)//n o
)//o p
;//p q
}00 
if22 
(22 
!22 
context22 
.22 
Set22 
<22 
TaskUnitType22 )
>22) *
(22* +
)22+ ,
.22, -
Any22- 0
(220 1
)221 2
)222 3
{33 
context44 
.44 
Set44 
<44 
TaskUnitType44 (
>44( )
(44) *
)44* +
.44+ ,
Add44, /
(44/ 0
new440 3
TaskUnitType444 @
(44@ A
$num44A B
,44B C
nameof44D J
(44J K
TaskUnitType44K W
.44W X
TypeOne44X _
)44_ `
)44` a
)44a b
;44b c
context55 
.55 
Set55 
<55 
TaskUnitType55 (
>55( )
(55) *
)55* +
.55+ ,
Add55, /
(55/ 0
new550 3
TaskUnitType554 @
(55@ A
$num55A B
,55B C
nameof55D J
(55J K
TaskUnitType55K W
.55W X
TypeTwo55X _
)55_ `
)55` a
)55a b
;55b c
}66 
await88 
context88 
.88 
SaveChangesAsync88 *
(88* +
cancellationToken88+ <
)88< =
;88= >
}99 	
}:: 
public<< 

override<< 
async<< 
Task<< 
	StopAsync<< (
(<<( )
CancellationToken<<) :
cancellationToken<<; L
)<<L M
{== 
_logger>> 
.>> 
LogInformation>> 
(>> 
$str>> @
)>>@ A
;>>A B
await@@ 
base@@ 
.@@ 
	StopAsync@@ 
(@@ 
cancellationToken@@ .
)@@. /
;@@/ 0
}AA 
}BB ”!
pC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\AntiCorruption\HostedServices\TaskProcessingBackgroundService.cs
	namespace		 	
SJI3		
 
.		 
Infrastructure		 
.		 
AntiCorruption		 ,
.		, -
HostedServices		- ;
;		; <
public 
class +
TaskProcessingBackgroundService ,
:- .
BackgroundService/ @
{ 
private 
readonly 
IServiceProvider %
_serviceProvider& 6
;6 7
private 
readonly 

ITaskQueue 
<  
Guid  $
>$ %

_taskQueue& 0
;0 1
private 
readonly 

IAppLogger 
<  +
TaskProcessingBackgroundService  ?
>? @
_loggerA H
;H I
public 
+
TaskProcessingBackgroundService *
(* +
IServiceProvider+ ;
serviceProvider< K
,K L

ITaskQueueM W
<W X
GuidX \
>\ ]
	taskQueue^ g
,g h

IAppLogger 
< +
TaskProcessingBackgroundService 2
>2 3
logger4 :
): ;
{ 
_serviceProvider 
= 
serviceProvider *
;* +

_taskQueue 
= 
	taskQueue 
; 
_logger 
= 
logger 
; 
} 
	protected 
override 
async 
Task !
ExecuteAsync" .
(. /
CancellationToken/ @
stoppingTokenA N
)N O
{ 
_logger 
. 
LogInformation 
( 
$str A
)A B
;B C
await  
BackgroundProcessing "
(" #
stoppingToken# 0
)0 1
;1 2
} 
private   
async   
Task    
BackgroundProcessing   +
(  + ,
CancellationToken  , =
cancellationToken  > O
)  O P
{!! 
while"" 
("" 
!"" 
cancellationToken"" !
.""! "#
IsCancellationRequested""" 9
)""9 :
{## 	
var$$ 
workItem$$ 
=$$ 
await%% 

_taskQueue%%  
.%%  !
DequeueAsync%%! -
(%%- .
cancellationToken%%. ?
)%%? @
;%%@ A
try'' 
{(( 
using)) 
var)) 
scope)) 
=))  !
_serviceProvider))" 2
.))2 3
CreateScope))3 >
())> ?
)))? @
;))@ A
var** 
processingService** %
=**& '
scope**( -
.**- .
ServiceProvider**. =
.**= >
GetRequiredService**> P
<**P Q"
ITaskProcessingService**Q g
>**g h
(**h i
)**i j
;**j k
await++ 
processingService++ '
.++' (
ProcessTask++( 3
(++3 4
await++4 9
workItem++: B
(++B C
cancellationToken++C T
)++T U
,++U V
cancellationToken++W h
)++h i
;++i j
},, 
catch-- 
(-- 
	Exception-- 
ex-- 
)--  
{.. 
_logger// 
.// 
LogError//  
(//  !
ex//! #
,//# $
$str00 9
,009 :
nameof00; A
(00A B
workItem00B J
)00J K
)00K L
;00L M
}11 
}22 	
}33 
public55 

override55 
async55 
Task55 
	StopAsync55 (
(55( )
CancellationToken55) :
cancellationToken55; L
)55L M
{66 
_logger77 
.77 
LogInformation77 
(77 
$str77 B
)77B C
;77C D
await99 
base99 
.99 
	StopAsync99 
(99 
cancellationToken99 .
)99. /
;99/ 0
}:: 
};; ≠
\C:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\AntiCorruption\HttpClients\IDemoClientApi.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
AntiCorruption ,
., -
HttpClients- 8
;8 9
public 
	interface 
IDemoClientApi 
{		 
[

 
Get

 
(

 	
$str

	 
)

 
]

 
Task 
< 	
HttpResponseMessage	 
> 
Get !
(! "
)" #
;# $
[ 
Post 	
(	 

$str
 
) 
] 
Task 
< 	
HttpResponseMessage	 
> 
Post "
(" #
[# $
Body$ (
]( )
DemoPostBody* 6
frBody7 =
)= >
;> ?
} ó
cC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\AntiCorruption\HttpClients\Requests\DemoPostBody.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
AntiCorruption ,
., -
HttpClients- 8
.8 9
Requests9 A
;A B
public 
class 
DemoPostBody 
{ 
[ 
JsonPropertyName 
( 
$str 
) 
] 
public		 

Guid		 
Id		 
{		 
get		 
;		 
set		 
;		 
}		  
}

 ∏
_C:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Consumers\Handlers\TaskStatusChangedConsumer.cs
	namespace

 	
SJI3


 
.

 
Infrastructure

 
.

 
	Consumers

 '
.

' (
Handlers

( 0
;

0 1
public 
class %
TaskStatusChangedConsumer &
:' (
	IConsumer) 2
<2 3
TaskStatusChanged3 D
>D E
{ 
private 
readonly 
IHubContext  
<  !
NotificationsHub! 1
>1 2
_hubContext3 >
;> ?
private 
readonly 

IAppLogger 
<  %
TaskStatusChangedConsumer  9
>9 :
_logger; B
;B C
public 
%
TaskStatusChangedConsumer $
($ %
IHubContext 
< 
NotificationsHub $
>$ %

hubContext& 0
,0 1

IAppLogger 
< %
TaskStatusChangedConsumer ,
>, -
logger. 4
)4 5
{ 
_hubContext 
= 

hubContext  
??! #
throw$ )
new* -!
ArgumentNullException. C
(C D
nameofD J
(J K

hubContextK U
)U V
)V W
;W X
_logger 
= 
logger 
?? 
throw !
new" %!
ArgumentNullException& ;
(; <
nameof< B
(B C
loggerC I
)I J
)J K
;K L
} 
public 

async 
Task 
Consume 
( 
ConsumeContext ,
<, -
TaskStatusChanged- >
>> ?
context@ G
)G H
{ 
_logger 
. 
LogInformation 
( 
$str n
,n o
contextp w
.w x
Messagex 
.	 Ä
Id
Ä Ç
,
Ç É
context
Ñ ã
.
ã å
Message
å ì
)
ì î
;
î ï
await 
_hubContext 
. 
Clients !
. 
Group 
( 
context 
. 
Message "
." #
ApplicationUserId# 4
)4 5
. 
	SendAsync 
( 
$str )
,) *
new+ .
{/ 0
context1 8
.8 9
Message9 @
.@ A
TaskIdA G
,G H
contextI P
.P Q
MessageQ X
.X Y

TaskStatusY c
}d e
)e f
;f g
}   
}!! ≤	
iC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Consumers\Handlers\TaskStatusChangedConsumerDefinition.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
	Consumers '
.' (
Handlers( 0
{ 
public 

class /
#TaskStatusChangedConsumerDefinition 4
:5 6
ConsumerDefinition7 I
<I J%
TaskStatusChangedConsumerJ c
>c d
{ 
	protected 
override 
void 
ConfigureConsumer  1
(1 2(
IReceiveEndpointConfigurator2 N 
endpointConfiguratorO c
,c d!
IConsumerConfiguratore z
<z {&
TaskStatusChangedConsumer	{ î
>
î ï"
consumerConfigurator
ñ ™
)
™ ´
{ 	 
endpointConfigurator		  
.		  !
UseMessageRetry		! 0
(		0 1
r		1 2
=>		3 5
r		6 7
.		7 8
	Intervals		8 A
(		A B
$num		B E
,		E F
$num		G K
)		K L
)		L M
;		M N
}

 	
} 
} ∫
VC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Consumers\Messages\IntegrationEvent.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
	Consumers '
.' (
Messages( 0
{ 
public 

record 
IntegrationEvent "
{ 
public 
IntegrationEvent 
(  
)  !
{		 	
Id

 
=

 
Guid

 
.

 
NewGuid

 
(

 
)

 
;

  
CreationDate 
= 
DateTime #
.# $
UtcNow$ *
;* +
} 	
[ 	
JsonConstructor	 
] 
public 
IntegrationEvent 
(  
Guid  $
id% '
,' (
DateTime) 1

createDate2 <
)< =
{ 	
Id 
= 
id 
; 
CreationDate 
= 

createDate %
;% &
} 	
[ 	
JsonInclude	 
] 
public 
Guid 
Id 
{ 
get 
; 
private %
init& *
;* +
}, -
[ 	
JsonInclude	 
] 
public 
DateTime 
CreationDate $
{% &
get' *
;* +
private, 3
init4 8
;8 9
}: ;
} 
} ƒ
WC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Consumers\Messages\TaskStatusChanged.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
	Consumers '
.' (
Messages( 0
;0 1
public 
record 
TaskStatusChanged 
(  
Guid  $
TaskId% +
,+ ,
int- 0

TaskStatus1 ;
,; <
string= C
ApplicationUserIdD U
)U V
:W X
IntegrationEventY i
;i j≠7
DC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Data\AppDbContext.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Data "
;" #
public 
class 
AppDbContext 
: 
	DbContext %
{ 
public 

DbSet 
< 
ApplicationUser  
>  !
ApplicationUsers" 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 

DbSet 
< 
TaskUnit 
> 
	TaskUnits $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 

DbSet 
< 
TaskUnitType 
> 
TaskUnitTypes ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 

DbSet 
< 
TaskUnitStatus 
>  
TaskUnitStatuses! 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 

AppDbContext 
( 
DbContextOptions (
<( )
AppDbContext) 5
>5 6
options7 >
)> ?
:@ A
baseB F
(F G
optionsG N
)N O
{ 
} 
public 

override 
Task 
< 
int 
> 
SaveChangesAsync .
(. /
CancellationToken/ @
cancellationTokenA R
=S T
newU X
CancellationTokenY j
(j k
)k l
)l m
{ 
foreach 
( 
var 
entity 
in 
ChangeTracker ,
., -
Entries- 4
<4 5
IAudit5 ;
>; <
(< =
)= >
)> ?
{ 	
if 
( 
entity 
. 
State 
is 
EntityState  +
.+ ,
Added, 1
or2 4
EntityState5 @
.@ A
ModifiedA I
)I J
{ 
if 
( 
entity 
. 
State  
==! #
EntityState$ /
./ 0
Modified0 8
)8 9
{   
entity!! 
.!! 
Entity!! !
.!!! "
SetModifiedOn!!" /
(!!/ 0
DateTime!!0 8
.!!8 9
UtcNow!!9 ?
)!!? @
;!!@ A
}"" 
if$$ 
($$ 
entity$$ 
.$$ 
State$$  
==$$! #
EntityState$$$ /
.$$/ 0
Added$$0 5
)$$5 6
{%% 
entity&& 
.&& 
Entity&& !
.&&! "
SetCreatedOn&&" .
(&&. /
DateTime&&/ 7
.&&7 8
UtcNow&&8 >
)&&> ?
;&&? @
}'' 
}(( 
})) 	
return++ 
base++ 
.++ 
SaveChangesAsync++ $
(++$ %
cancellationToken++% 6
)++6 7
;++7 8
},, 
	protected.. 
override.. 
void.. 
OnModelCreating.. +
(..+ ,
ModelBuilder.., 8
modelBuilder..9 E
)..E F
{// 
modelBuilder00 
.00 
Entity00 
<00 
ApplicationUser00 +
>00+ ,
(00, -
)00- .
.11 
HasKey11 
(11 
a11 
=>11 
a11 
.11 
Id11 
)11 
;11 
modelBuilder33 
.44 
Entity44 
<44 
ApplicationUser44 #
>44# $
(44$ %
)44% &
.55 
Property55 
<55 
List55 
<55 
Guid55 
>55  
>55  !
(55! "
$str55" 4
)554 5
.66 
HasConversion66 
(66 
ids77 
=>77 
JsonSerializer77 %
.77% &
	Serialize77& /
(77/ 0
ids770 3
.773 4
Select774 :
(77: ;
id77; =
=>77> @
id77A C
)77C D
,77D E
new77F I!
JsonSerializerOptions77J _
(77_ `
)77` a
)77a b
,77b c
idValue88 
=>88 
JsonSerializer88 )
.88) *
Deserialize88* 5
<885 6
List886 :
<88: ;
Guid88; ?
>88? @
>88@ A
(88A B
idValue88B I
,88I J
new88K N!
JsonSerializerOptions88O d
(88d e
)88e f
)88f g
.88g h
Select88h n
(88n o
id88o q
=>88r t
id88u w
)88w x
.88x y
Distinct	88y Å
(
88Å Ç
)
88Ç É
.
88É Ñ
ToList
88Ñ ä
(
88ä ã
)
88ã å
,
88å ç
new99 
ValueComparer99 !
<99! "
List99" &
<99& '
Guid99' +
>99+ ,
>99, -
(99- .
(:: 
roleId1:: 
,:: 
roleId2:: %
)::% &
=>::' )
roleId1::* 1
.::1 2
Equals::2 8
(::8 9
roleId2::9 @
)::@ A
,::A B
c;; 
=>;; 
c;; 
.;; 
	Aggregate;; $
(;;$ %
$num;;% &
,;;& '
(;;( )
a;;) *
,;;* +
v;;, -
);;- .
=>;;/ 1
HashCode;;2 :
.;;: ;
Combine;;; B
(;;B C
a;;C D
,;;D E
v;;F G
.;;G H
GetHashCode;;H S
(;;S T
);;T U
);;U V
);;V W
,;;W X
c<< 
=><< 
c<< 
.<< 
ToList<< !
(<<! "
)<<" #
)<<# $
)<<$ %
;<<% &
modelBuilder>> 
.?? 
Entity?? 
<?? 
ApplicationUser?? #
>??# $
(??$ %
)??% &
.@@ 
Ignore@@ 
(@@ 
a@@ 
=>@@ 
a@@ 
.@@ 
	TaskUnits@@ $
)@@$ %
;@@% &
modelBuilderBB 
.BB 
EntityBB 
<BB 
TaskUnitBB $
>BB$ %
(BB% &
)BB& '
.CC 
HasOneCC 
<CC 
ApplicationUserCC #
>CC# $
(CC$ %
)CC% &
.DD 
WithManyDD 
(DD 
)DD 
.EE 
HasForeignKeyEE 
(EE 
sEE 
=>EE 
sEE  !
.EE! "
ApplicationUserIdEE" 3
)EE3 4
;EE4 5
}FF 
}GG û
BC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Data\UnitOfWork.cs
	namespace

 	
SJI3


 
.

 
Infrastructure

 
.

 
Data

 "
;

" #
public 
class 

UnitOfWork 
< 
TContext  
>  !
:" #
IUnitOfWork$ /
where0 5
TContext6 >
:? @
	DbContextA J
{ 
private 
readonly 
TContext 
_context &
;& '
private 

Dictionary 
< 
Type 
, 
object #
># $
_repositories% 2
;2 3
public 


UnitOfWork 
( 
TContext 
context &
)& '
{ 
_context 
= 
context 
?? 
throw #
new$ '!
ArgumentNullException( =
(= >
nameof> D
(D E
contextE L
)L M
)M N
;N O
} 
public 

async 
Task 
< 
int 
> 
CommitAsync &
(& '
CancellationToken' 8
cancellationToken9 J
=K L
defaultM T
)T U
{ 
return 
await 
_context 
. 
SaveChangesAsync .
(. /
cancellationToken/ @
)@ A
;A B
} 
public 

IGenericRepository 
< 
TEntity %
,% &
TKey' +
>+ ,

Repository- 7
<7 8
TEntity8 ?
,? @
TKeyA E
>E F
(F G
)G H
whereI N
TEntityO V
:W X
EntityY _
<_ `
TKey` d
>d e
{ 
_repositories 
??= 
new 

Dictionary (
<( )
Type) -
,- .
object/ 5
>5 6
(6 7
)7 8
;8 9
var 
type 
= 
typeof 
( 
TEntity !
)! "
;" #
if 

( 
! 
_repositories 
. 
ContainsKey &
(& '
type' +
)+ ,
), -
_repositories. ;
[; <
type< @
]@ A
=B C
newD G
GenericRepositoryH Y
<Y Z
TEntityZ a
,a b
TKeyc g
>g h
(h i
_contexti q
)q r
;r s
return   
(   
IGenericRepository   "
<  " #
TEntity  # *
,  * +
TKey  , 0
>  0 1
)  1 2
_repositories  2 ?
[  ? @
type  @ D
]  D E
;  E F
}!! 
}"" €
HC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Filters\LoggingFilter.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Filters %
;% &
public 
class 
LoggingFilter 
< 
T 
> 
: 
IFilter		 
<		 
ConsumeContext		 
<		 
T		 
>		 
>		 
where

 	
T


 
:

 
class

 
{ 
private 
readonly 

IAppLogger 
<  
LoggingFilter  -
<- .
T. /
>/ 0
>0 1
_logger2 9
;9 :
public 

LoggingFilter 
( 

IAppLogger #
<# $
LoggingFilter$ 1
<1 2
T2 3
>3 4
>4 5
logger6 <
)< =
{ 
_logger 
= 
logger 
; 
} 
public 

void 
Probe 
( 
ProbeContext "
context# *
)* +
{ 
throw 
new !
NotSupportedException '
(' (
)( )
;) *
} 
public 

Task 
Send 
( 
ConsumeContext #
<# $
T$ %
>% &
context' .
,. /
IPipe0 5
<5 6
ConsumeContext6 D
<D E
TE F
>F G
>G H
nextI M
)M N
{ 
_logger 
. 
LogInformation 
( 
$" !
$str! *
{* +
typeof+ 1
(1 2
T2 3
)3 4
.4 5
Name5 9
}9 :
": ;
); <
;< =
var 
response 
= 
next 
. 
Send  
(  !
context! (
)( )
;) *
_logger 
. 
LogInformation 
( 
$" !
$str! )
{) *
typeof* 0
(0 1
T1 2
)2 3
.3 4
Name4 8
}8 9
"9 :
): ;
;; <
return 
response 
; 
} 
} ‘
IC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Interfaces\IUnitOfWork.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 

Interfaces (
;( )
public 
	interface 
IUnitOfWork 
< 
out  
TContext! )
>) *
:+ ,
IUnitOfWork- 8
where9 >
TContext? G
:H I
	DbContextJ S
{ 
TContext 
Context 
{ 
get 
; 
} 
}		 Õ
JC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\JobScheduler\JobFactory.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
JobScheduler *
;* +
public 
class 

JobFactory 
: 
IJobFactory %
{		 
private

 
readonly

 
IServiceProvider

 %
_serviceProvider

& 6
;

6 7
public 


JobFactory 
( 
IServiceProvider &
serviceProvider' 6
)6 7
{ 
_serviceProvider 
= 
serviceProvider *
;* +
} 
public 

IJob 
NewJob 
( 
TriggerFiredBundle )
bundle* 0
,0 1

IScheduler2 <
	scheduler= F
)F G
{ 
return 
_serviceProvider 
.  
GetRequiredService  2
(2 3
bundle3 9
.9 :
	JobDetail: C
.C D
JobTypeD K
)K L
asM O
IJobP T
;T U
} 
public 

void 
	ReturnJob 
( 
IJob 
job "
)" #
{ 
throw 
new !
NotSupportedException '
(' (
)( )
;) *
} 
} ó
KC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\JobScheduler\JobSchedule.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
JobScheduler *
;* +
public 
class 
JobSchedule 
{ 
public 

JobSchedule 
( 
Type 
jobType #
,# $
string% +
cronExpression, :
): ;
{ 
JobType		 
=		 
jobType		 
;		 
CronExpression

 
=

 
cronExpression

 '
;

' (
} 
public 

Type 
JobType 
{ 
get 
; 
}  
public 

string 
CronExpression  
{! "
get# &
;& '
}( )
} ‰
HC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Logging\LoggerAdapter.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Logging %
;% &
public 
class 
LoggerAdapter 
< 
T 
> 
: 

IAppLogger  *
<* +
T+ ,
>, -
{ 
private		 
readonly		 
ILogger		 
<		 
T		 
>		 
_logger		  '
;		' (
public 

LoggerAdapter 
( 
ILoggerFactory '
loggerFactory( 5
)5 6
{ 
_logger 
= 
loggerFactory 
.  
CreateLogger  ,
<, -
T- .
>. /
(/ 0
)0 1
;1 2
} 
public 

void 
LogInformation 
( 
string %
message& -
,- .
params/ 5
object6 <
[< =
]= >
args? C
)C D
{ 
_logger 
. 
LogInformation 
( 
message &
,& '
args( ,
), -
;- .
} 
public 

void 

LogWarning 
( 
	Exception $
	exception% .
,. /
string0 6
message7 >
,> ?
params@ F
objectG M
[M N
]N O
argsP T
)T U
{ 
_logger 
. 

LogWarning 
( 
	exception $
,$ %
message& -
,- .
args/ 3
)3 4
;4 5
} 
public 

void 
LogError 
( 
	Exception "
	exception# ,
,, -
string. 4
message5 <
,< =
params> D
objectE K
[K L
]L M
argsN R
)R S
{ 
_logger 
. 
LogError 
( 
	exception "
," #
message$ +
,+ ,
args- 1
)1 2
;2 3
} 
} ØQ
TC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Migrations\20220913111859_Initial.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 

Migrations (
{		 
public

 

partial

 
class

 
Initial

  
:

! "
	Migration

# ,
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str (
,( )
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
Guid& *
>* +
(+ ,
type, 0
:0 1
$str2 8
,8 9
nullable: B
:B C
falseD I
)I J
,J K
UserName 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
trueL P
)P Q
,Q R
Password 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
trueL P
)P Q
,Q R
TaskUnitsPrivate $
=% &
table' ,
., -
Column- 3
<3 4
string4 :
>: ;
(; <
type< @
:@ A
$strB H
,H I
nullableJ R
:R S
trueT X
)X Y
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% :
,: ;
x< =
=>> @
xA B
.B C
IdC E
)E F
;F G
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str (
,( )
columns 
: 
table 
=> !
new" %
{ 
Id   
=   
table   
.   
Column   %
<  % &
int  & )
>  ) *
(  * +
type  + /
:  / 0
$str  1 :
,  : ;
nullable  < D
:  D E
false  F K
)  K L
.!! 

Annotation!! #
(!!# $
$str!!$ D
,!!D E)
NpgsqlValueGenerationStrategy!!F c
.!!c d#
IdentityByDefaultColumn!!d {
)!!{ |
,!!| }
Name"" 
="" 
table""  
.""  !
Column""! '
<""' (
string""( .
>"". /
(""/ 0
type""0 4
:""4 5
$str""6 <
,""< =
nullable""> F
:""F G
true""H L
)""L M
}## 
,## 
constraints$$ 
:$$ 
table$$ "
=>$$# %
{%% 
table&& 
.&& 

PrimaryKey&& $
(&&$ %
$str&&% :
,&&: ;
x&&< =
=>&&> @
x&&A B
.&&B C
Id&&C E
)&&E F
;&&F G
}'' 
)'' 
;'' 
migrationBuilder)) 
.)) 
CreateTable)) (
())( )
name** 
:** 
$str** %
,**% &
columns++ 
:++ 
table++ 
=>++ !
new++" %
{,, 
Id-- 
=-- 
table-- 
.-- 
Column-- %
<--% &
int--& )
>--) *
(--* +
type--+ /
:--/ 0
$str--1 :
,--: ;
nullable--< D
:--D E
false--F K
)--K L
... 

Annotation.. #
(..# $
$str..$ D
,..D E)
NpgsqlValueGenerationStrategy..F c
...c d#
IdentityByDefaultColumn..d {
)..{ |
,..| }
Name// 
=// 
table//  
.//  !
Column//! '
<//' (
string//( .
>//. /
(/// 0
type//0 4
://4 5
$str//6 <
,//< =
nullable//> F
://F G
true//H L
)//L M
}00 
,00 
constraints11 
:11 
table11 "
=>11# %
{22 
table33 
.33 

PrimaryKey33 $
(33$ %
$str33% 7
,337 8
x339 :
=>33; =
x33> ?
.33? @
Id33@ B
)33B C
;33C D
}44 
)44 
;44 
migrationBuilder66 
.66 
CreateTable66 (
(66( )
name77 
:77 
$str77 !
,77! "
columns88 
:88 
table88 
=>88 !
new88" %
{99 
Id:: 
=:: 
table:: 
.:: 
Column:: %
<::% &
Guid::& *
>::* +
(::+ ,
type::, 0
:::0 1
$str::2 8
,::8 9
nullable::: B
:::B C
false::D I
)::I J
,::J K
Moniker;; 
=;; 
table;; #
.;;# $
Column;;$ *
<;;* +
string;;+ 1
>;;1 2
(;;2 3
type;;3 7
:;;7 8
$str;;9 ?
,;;? @
nullable;;A I
:;;I J
true;;K O
);;O P
,;;P Q
FromDateTime<<  
=<<! "
table<<# (
.<<( )
Column<<) /
<<</ 0
LocalDateTime<<0 =
><<= >
(<<> ?
type<<? C
:<<C D
$str<<E b
,<<b c
nullable<<d l
:<<l m
true<<n r
)<<r s
,<<s t

ToDateTime== 
===  
table==! &
.==& '
Column==' -
<==- .
LocalDateTime==. ;
>==; <
(==< =
type=== A
:==A B
$str==C `
,==` a
nullable==b j
:==j k
true==l p
)==p q
,==q r
TaskUnitTypeId>> "
=>># $
table>>% *
.>>* +
Column>>+ 1
<>>1 2
int>>2 5
>>>5 6
(>>6 7
type>>7 ;
:>>; <
$str>>= F
,>>F G
nullable>>H P
:>>P Q
false>>R W
)>>W X
,>>X Y
TaskUnitStatusId?? $
=??% &
table??' ,
.??, -
Column??- 3
<??3 4
int??4 7
>??7 8
(??8 9
type??9 =
:??= >
$str??? H
,??H I
nullable??J R
:??R S
false??T Y
)??Y Z
,??Z [
ApplicationUserId@@ %
=@@& '
table@@( -
.@@- .
Column@@. 4
<@@4 5
Guid@@5 9
>@@9 :
(@@: ;
type@@; ?
:@@? @
$str@@A G
,@@G H
nullable@@I Q
:@@Q R
false@@S X
)@@X Y
,@@Y Z
	CreatedOnAA 
=AA 
tableAA  %
.AA% &
ColumnAA& ,
<AA, -
DateTimeOffsetAA- ;
>AA; <
(AA< =
typeAA= A
:AAA B
$strAAC ]
,AA] ^
nullableAA_ g
:AAg h
falseAAi n
)AAn o
,AAo p

ModifiedOnBB 
=BB  
tableBB! &
.BB& '
ColumnBB' -
<BB- .
DateTimeOffsetBB. <
>BB< =
(BB= >
typeBB> B
:BBB C
$strBBD ^
,BB^ _
nullableBB` h
:BBh i
trueBBj n
)BBn o
}CC 
,CC 
constraintsDD 
:DD 
tableDD "
=>DD# %
{EE 
tableFF 
.FF 

PrimaryKeyFF $
(FF$ %
$strFF% 3
,FF3 4
xFF5 6
=>FF7 9
xFF: ;
.FF; <
IdFF< >
)FF> ?
;FF? @
tableGG 
.GG 

ForeignKeyGG $
(GG$ %
nameHH 
:HH 
$strHH O
,HHO P
columnII 
:II 
xII  !
=>II" $
xII% &
.II& '
ApplicationUserIdII' 8
,II8 9
principalTableJJ &
:JJ& '
$strJJ( :
,JJ: ;
principalColumnKK '
:KK' (
$strKK) -
,KK- .
onDeleteLL  
:LL  !
ReferentialActionLL" 3
.LL3 4
CascadeLL4 ;
)LL; <
;LL< =
}MM 
)MM 
;MM 
migrationBuilderOO 
.OO 
CreateIndexOO (
(OO( )
namePP 
:PP 
$strPP 6
,PP6 7
tableQQ 
:QQ 
$strQQ "
,QQ" #
columnRR 
:RR 
$strRR +
)RR+ ,
;RR, -
}SS 	
	protectedUU 
overrideUU 
voidUU 
DownUU  $
(UU$ %
MigrationBuilderUU% 5
migrationBuilderUU6 F
)UUF G
{VV 	
migrationBuilderWW 
.WW 
	DropTableWW &
(WW& '
nameXX 
:XX 
$strXX !
)XX! "
;XX" #
migrationBuilderZZ 
.ZZ 
	DropTableZZ &
(ZZ& '
name[[ 
:[[ 
$str[[ (
)[[( )
;[[) *
migrationBuilder]] 
.]] 
	DropTable]] &
(]]& '
name^^ 
:^^ 
$str^^ %
)^^% &
;^^& '
migrationBuilder`` 
.`` 
	DropTable`` &
(``& '
nameaa 
:aa 
$straa (
)aa( )
;aa) *
}bb 	
}cc 
}dd ‘"
QC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Repositories\GenericRepository.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Repositories *
;* +
public

 
class

 
GenericRepository

 
<

 
T

  
,

  !
TKey

" &
>

& '
:

( )
IGenericRepository

* <
<

< =
T

= >
,

> ?
TKey

@ D
>

D E
where

F K
T

L M
:

N O
Entity

P V
<

V W
TKey

W [
>

[ \
{ 
private 
readonly 
	DbContext 
_context '
;' (
public 

GenericRepository 
( 
	DbContext &
context' .
). /
{ 
_context 
= 
context 
; 
} 
public 

async 
Task 
< 
T 
> 
GetByIdAsync %
(% &
TKey& *
id+ -
)- .
{ 
return 
await 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
.& '
	FindAsync' 0
(0 1
id1 3
)3 4
;4 5
} 
public 

async 
Task 
< 
IReadOnlyList #
<# $
T$ %
>% &
>& '
ListAllAsync( 4
(4 5
)5 6
{ 
return 
await 
_context 
. 
Set !
<! "
T" #
># $
($ %
)% &
.& '
AsNoTracking' 3
(3 4
)4 5
.5 6
ToListAsync6 A
(A B
)B C
;C D
} 
public 

async 
Task 
AddAsync 
( 
T  
entity! '
)' (
{ 
await 
_context 
. 
Set 
< 
T 
> 
( 
) 
.  
AddAsync  (
(( )
entity) /
)/ 0
;0 1
}   
public"" 

void"" 
AddRangeAsync"" 
("" 
List"" "
<""" #
T""# $
>""$ %
entities""& .
)"". /
{## 
_context$$ 
.$$ 
Set$$ 
<$$ 
T$$ 
>$$ 
($$ 
)$$ 
.$$ 
AddRangeAsync$$ '
($$' (
entities$$( 0
)$$0 1
;$$1 2
}%% 
public'' 

void'' 
Update'' 
('' 
T'' 
entity'' 
)''  
{(( 
_context)) 
.)) 
Set)) 
<)) 
T)) 
>)) 
()) 
))) 
.)) 
Update))  
())  !
entity))! '
)))' (
;))( )
}** 
public,, 

void,, 
UpdateRange,, 
(,, 
List,,  
<,,  !
T,,! "
>,," #
entities,,$ ,
),,, -
{-- 
_context.. 
... 
Set.. 
<.. 
T.. 
>.. 
(.. 
).. 
... 
UpdateRange.. %
(..% &
entities..& .
)... /
;../ 0
}// 
public11 

void11 
Delete11 
(11 
T11 
entity11 
)11  
{22 
_context33 
.33 
Set33 
<33 
T33 
>33 
(33 
)33 
.33 
Remove33  
(33  !
entity33! '
)33' (
;33( )
}44 
public66 


IQueryable66 
<66 
T66 
>66 
Query66 
(66 
)66  
{77 
return88 
_context88 
.88 
Set88 
<88 
T88 
>88 
(88 
)88  
;88  !
}99 
}:: —=
RC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Repositories\TaskUnitRepository.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Repositories *
;* +
public 
class 
TaskUnitRepository 
:  !
GenericRepository" 3
<3 4
TaskUnit4 <
,< =
Guid> B
>B C
,C D
GetE H
.H I
IRepositoryI T
,T U
ExistsV \
.\ ]
IRepository] h
,h i
Putj m
.m n
IRepositoryn y
{ 
private 
readonly 
AppDbContext !
_context" *
;* +
private 
readonly 

ITaskQueue 
<  
Guid  $
>$ %

_taskQueue& 0
;0 1
private 
readonly 
IBus 
_bus 
; 
public 

TaskUnitRepository 
( 
AppDbContext *
context+ 2
,2 3

ITaskQueue4 >
<> ?
Guid? C
>C D
	taskQueueE N
,N O
IBusP T
busU X
)X Y
:Z [
base\ `
(` a
contexta h
)h i
{ 
_context 
= 
context 
; 

_taskQueue 
= 
	taskQueue 
; 
_bus 
= 
bus 
; 
} 
public   

Task   
<   
bool   
>   
Exists   
(   
Guid   !
id  " $
)  $ %
{!! 
return"" 
_context"" 
."" 
Set"" 
<"" 
TaskUnit"" $
>""$ %
(""% &
)""& '
.""' (
AnyAsync""( 0
(""0 1
f""1 2
=>""3 5
f""6 7
.""7 8
Id""8 :
."": ;
Equals""; A
(""A B
id""B D
)""D E
)""E F
;""F G
}## 
public%% 

	PagedList%% 
<%% 
TaskUnit%% 
>%% 
Get%% "
(%%" #
Get%%# &
.%%& '
ResourceParameters%%' 9

parameters%%: D
)%%D E
{&& 

IQueryable'' 
<'' 
TaskUnit'' 
>'' 
query'' "
=''# $
_context''% -
.''- .
Set''. 1
<''1 2
TaskUnit''2 :
>'': ;
(''; <
)''< =
;''= >
if)) 

()) 
!)) 
string)) 
.)) 
IsNullOrEmpty)) !
())! "

parameters))" ,
.)), -
Start))- 2
)))2 3
&&))4 6
!))7 8
string))8 >
.))> ?
IsNullOrEmpty))? L
())L M

parameters))M W
.))W X
End))X [
)))[ \
)))\ ]
{** 	
var++ 
start++ 
=++ 
LocalDateTime++ %
.++% &
FromDateTime++& 2
(++2 3
DateTime++3 ;
.++; <
Parse++< A
(++A B

parameters++B L
.++L M
Start++M R
,++R S
null++T X
)++X Y
)++Y Z
;++Z [
var,, 
end,, 
=,, 
LocalDateTime,, #
.,,# $
FromDateTime,,$ 0
(,,0 1
DateTime,,1 9
.,,9 :
Parse,,: ?
(,,? @

parameters,,@ J
.,,J K
End,,K N
,,,N O
null,,P T
),,T U
),,U V
;,,V W
var.. 
interval.. 
=.. 
new.. 
DateInterval.. +
(..+ ,
start.., 1
...1 2
Date..2 6
,..6 7
end..8 ;
...; <
Date..< @
)..@ A
;..A B
query00 
=00 
query00 
.00 
Where00 
(00  
t00  !
=>00" $
t00% &
.00& '

ToDateTime00' 1
.001 2
HasValue002 :
&&00; =
t00> ?
.00? @
FromDateTime00@ L
.00L M
HasValue00M U
&&00V X
(00Y Z
interval00Z b
.00b c
Contains00c k
(00k l
t00l m
.00m n
FromDateTime00n z
.00z {
Value	00{ Ä
.
00Ä Å
Date
00Å Ö
)
00Ö Ü
||
00á â
interval
00ä í
.
00í ì
Contains
00ì õ
(
00õ ú
t
00ú ù
.
00ù û

ToDateTime
00û ®
.
00® ©
Value
00© Æ
.
00Æ Ø
Date
00Ø ≥
)
00≥ ¥
)
00¥ µ
)
00µ ∂
;
00∂ ∑
}11 	
if33 

(33 
string33 
.33 
IsNullOrEmpty33  
(33  !

parameters33! +
.33+ ,
OrderBy33, 3
)333 4
)334 5
return44 
	PagedList44 
<44 
TaskUnit44 %
>44% &
.44& '
Create44' -
(44- .
query44. 3
,443 4

parameters445 ?
.44? @

PageNumber44@ J
,44J K

parameters55 
.55 
PageSize55 #
)55# $
;55$ %
return77 
	PagedList77 
<77 
TaskUnit77 !
>77! "
.77" #
Create77# )
(77) *
query77* /
.77/ 0
OrderBy770 7
(777 8

parameters778 B
.77B C
OrderBy77C J
)77J K
,77K L

parameters77M W
.77W X

PageNumber77X b
,77b c

parameters88 
.88 
PageSize88 
)88  
;88  !
}99 
public;; 

async;; 
Task;; 
<;; 
bool;; 
>;; 
UpdateTaskStatus;; ,
(;;, -
TaskUnit;;- 5
taskUnit;;6 >
);;> ?
{<< 
_context== 
.== 
Set== 
<== 
TaskUnit== 
>== 
(== 
)==  
.==  !
Update==! '
(==' (
taskUnit==( 0
)==0 1
;==1 2
if?? 

(?? 
await?? 
_context?? 
.?? 
SaveChangesAsync?? +
(??+ ,
)??, -
>??. /
$num??0 1
)??1 2
{@@ 	
awaitAA 

_taskQueueAA 
.AA 
QueueWorkItemAsyncAA /
(AA/ 0
_AA0 1
=>AA2 4
newAA5 8
	ValueTaskAA9 B
<AAB C
GuidAAC G
>AAG H
(AAH I
taskUnitAAI Q
.AAQ R
IdAAR T
)AAT U
)AAU V
;AAV W
awaitCC 
_busCC 
.CC 
PublishCC 
(CC 
newCC "
TaskStatusChangedCC# 4
(CC4 5
taskUnitCC5 =
.CC= >
IdCC> @
,CC@ A
taskUnitCCB J
.CCJ K
TaskUnitStatusIdCCK [
,CC[ \
taskUnitCC] e
.CCe f
ApplicationUserIdCCf w
.CCw x
ToString	CCx Ä
(
CCÄ Å
$str
CCÅ Ñ
)
CCÑ Ö
)
CCÖ Ü
)
CCÜ á
;
CCá à
returnEE 
awaitEE 
TaskEE 
.EE 

FromResultEE (
(EE( )
trueEE) -
)EE- .
;EE. /
}FF 	
returnHH 
awaitHH 
TaskHH 
.HH 

FromResultHH $
(HH$ %
falseHH% *
)HH* +
;HH+ ,
}II 
}JJ ƒ
YC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Services\Notification\NotificationsHub.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Services &
.& '
Notification' 3
{ 
public 

class 
NotificationsHub !
:" #
Hub$ '
{ 
public		 
override		 
async		 
Task		 "
OnConnectedAsync		# 3
(		3 4
)		4 5
{

 	
if 
( 
Context 
. 
User 
is 
{  !
Identity! )
.) *
Name* .
:. /
{0 1
}2 3
}3 4
)4 5
await6 ;
Groups< B
.B C
AddToGroupAsyncC R
(R S
ContextS Z
.Z [
ConnectionId[ g
,g h
Contexti p
.p q
Userq u
.u v
Identityv ~
.~ 
Name	 É
)
É Ñ
;
Ñ Ö
await 
base 
. 
OnConnectedAsync '
(' (
)( )
;) *
} 	
public 
override 
async 
Task "
OnDisconnectedAsync# 6
(6 7
	Exception7 @
	exceptionA J
)J K
{ 	
if 
( 
Context 
. 
User 
is 
{  !
Identity! )
.) *
Name* .
:. /
{0 1
}2 3
}3 4
)4 5
await6 ;
Groups< B
.B C 
RemoveFromGroupAsyncC W
(W X
ContextX _
._ `
ConnectionId` l
,l m
Contextn u
.u v
Userv z
.z {
Identity	{ É
.
É Ñ
Name
Ñ à
)
à â
;
â ä
await 
base 
. 
OnDisconnectedAsync *
(* +
	exception+ 4
)4 5
;5 6
} 	
} 
} “
UC:\Users\fiyaz\Desktop\SJI3\SJI3.Infrastructure\Services\Queue\BackgroundTaskQueue.cs
	namespace 	
SJI3
 
. 
Infrastructure 
. 
Services &
.& '
Queue' ,
;, -
public

 
class

 
BackgroundTaskQueue

  
:

! "

ITaskQueue

# -
<

- .
Guid

. 2
>

2 3
{ 
private 
readonly 
Channel 
< 
Func !
<! "
CancellationToken" 3
,3 4
	ValueTask5 >
<> ?
Guid? C
>C D
>D E
>E F
_queueG M
;M N
public 

BackgroundTaskQueue 
( 
int "
capacity# +
)+ ,
{ 
var 
options 
= 
new !
BoundedChannelOptions /
(/ 0
capacity0 8
)8 9
{ 	
FullMode 
= "
BoundedChannelFullMode -
.- .
Wait. 2
} 	
;	 

_queue 
= 
Channel 
. 
CreateBounded &
<& '
Func' +
<+ ,
CancellationToken, =
,= >
	ValueTask? H
<H I
GuidI M
>M N
>N O
>O P
(P Q
optionsQ X
)X Y
;Y Z
} 
public 

	ValueTask 
QueueWorkItemAsync '
(' (
Func( ,
<, -
CancellationToken- >
,> ?
	ValueTask@ I
<I J
GuidJ N
>N O
>O P
workItemQ Y
)Y Z
{ 
if 

( 
workItem 
== 
null 
) 
{ 	
throw 
new !
ArgumentNullException +
(+ ,
nameof, 2
(2 3
workItem3 ;
); <
)< =
;= >
} 	
return 
WriteInternalAsync !
(! "
workItem" *
)* +
;+ ,
}   
private"" 
async"" 
	ValueTask"" 
WriteInternalAsync"" .
("". /
Func""/ 3
<""3 4
CancellationToken""4 E
,""E F
	ValueTask""G P
<""P Q
Guid""Q U
>""U V
>""V W
workItem""X `
)""` a
{## 
await$$ 
_queue$$ 
.$$ 
Writer$$ 
.$$ 

WriteAsync$$ &
($$& '
workItem$$' /
)$$/ 0
;$$0 1
}%% 
public'' 

async'' 
	ValueTask'' 
<'' 
Func'' 
<''  
CancellationToken''  1
,''1 2
	ValueTask''3 <
<''< =
Guid''= A
>''A B
>''B C
>''C D
DequeueAsync''E Q
(''Q R
CancellationToken''R c
cancellationToken''d u
)''u v
{(( 
var)) 
workItem)) 
=)) 
await)) 
_queue)) #
.))# $
Reader))$ *
.))* +
	ReadAsync))+ 4
())4 5
cancellationToken))5 F
)))F G
;))G H
return++ 
workItem++ 
;++ 
},, 
}-- 