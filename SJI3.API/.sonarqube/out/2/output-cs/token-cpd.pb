ˆ
7C:\Users\fiyaz\Desktop\SJI3\SJI3.API\Common\Envelope.cs
	namespace 	
SJI3
 
. 
API 
. 
Common 
; 
public 
class 
Envelope 
< 
T 
> 
{ 
public 

T 
Result 
{ 
get 
; 
} 
public 

string 
ErrorMessage 
{  
get! $
;$ %
}& '
public 

DateTime 
TimeGenerated !
{" #
get$ '
;' (
}) *
	protected		 
internal		 
Envelope		 
(		  
T		  !
result		" (
,		( )
string		* 0
errorMessage		1 =
)		= >
{

 
Result 
= 
result 
; 
ErrorMessage 
= 
errorMessage #
;# $
TimeGenerated 
= 
DateTime  
.  !
UtcNow! '
;' (
} 
} 
public 
sealed 
class 
Envelope 
: 
Envelope '
<' (
string( .
>. /
{ 
private 
Envelope 
( 
string 
errorMessage (
)( )
: 	
base
 
( 
null 
, 
errorMessage !
)! "
{ 
} 
public 

static 
Envelope 
< 
T 
> 
Ok  
<  !
T! "
>" #
(# $
T$ %
result& ,
), -
{ 
return 
new 
Envelope 
< 
T 
> 
( 
result %
,% &
null' +
)+ ,
;, -
} 
public 

static 
Envelope 
Ok 
( 
) 
{ 
return 
new 
Envelope 
( 
null  
)  !
;! "
}   
public"" 

static"" 
Envelope"" 
Error""  
(""  !
string""! '
errorMessage""( 4
)""4 5
{## 
return$$ 
new$$ 
Envelope$$ 
($$ 
errorMessage$$ (
)$$( )
;$$) *
}%% 
}&& £
?C:\Users\fiyaz\Desktop\SJI3\SJI3.API\Common\ExceptionHandler.cs
	namespace 	
SJI3
 
. 
API 
. 
Common 
; 
public 
class 
ExceptionHandler 
{ 
private 
readonly 
RequestDelegate $
_next% *
;* +
private		 
readonly		 
ILogger		 
<		 
ExceptionHandler		 -
>		- .
_logger		/ 6
;		6 7
public 

ExceptionHandler 
( 
RequestDelegate +
next, 0
,0 1
ILogger2 9
<9 :
ExceptionHandler: J
>J K
loggerL R
)R S
{ 
_next 
= 
next 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
Invoke 
( 
HttpContext (
context) 0
)0 1
{ 
try 
{ 	
await 
_next 
( 
context 
)  
;  !
} 	
catch 
( 
	Exception 
ex 
) 
{ 	
await  
HandleExceptionAsync &
(& '
context' .
,. /
ex0 2
)2 3
;3 4
} 	
} 
private 
Task  
HandleExceptionAsync %
(% &
HttpContext& 1
context2 9
,9 :
	Exception; D
	exceptionE N
)N O
{ 
_logger 
. 

LogWarning 
( 
	exception $
.$ %
Message% ,
), -
;- .
string   
result   
=   
JsonSerializer   &
.  & '
	Serialize  ' 0
(  0 1
Envelope  1 9
.  9 :
Error  : ?
(  ? @
	exception  @ I
.  I J
Message  J Q
)  Q R
)  R S
;  S T
context!! 
.!! 
Response!! 
.!! 
ContentType!! $
=!!% &
$str!!' 9
;!!9 :
context"" 
."" 
Response"" 
."" 

StatusCode"" #
=""$ %
(""& '
int""' *
)""* +
HttpStatusCode""+ 9
.""9 :
InternalServerError"": M
;""M N
return## 
context## 
.## 
Response## 
.##  

WriteAsync##  *
(##* +
result##+ 1
)##1 2
;##2 3
}$$ 
}%% ÿ
KC:\Users\fiyaz\Desktop\SJI3\SJI3.API\Common\IntegrationApisConfiguration.cs
	namespace 	
SJI3
 
. 
API 
. 
Common 
; 
public 
class (
IntegrationApisConfiguration )
{ 
public 

string !
DemoClientBaseAddress '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} ˘
BC:\Users\fiyaz\Desktop\SJI3\SJI3.API\Controllers\BaseController.cs
	namespace 	
SJI3
 
. 
API 
. 
Controllers 
; 
public 
class 
BaseController 
: 
ControllerBase ,
{ 
	protected		 
new		 
IActionResult		 
Ok		  "
(		" #
)		# $
{

 
return 
base 
. 
Ok 
( 
Envelope 
.  
Ok  "
(" #
)# $
)$ %
;% &
} 
	protected 
IActionResult 
Ok 
< 
T  
>  !
(! "
T" #
result$ *
)* +
{ 
return 
base 
. 
Ok 
( 
Envelope 
.  
Ok  "
(" #
result# )
)) *
)* +
;+ ,
} 
	protected 
IActionResult 
Error !
(! "
string" (
errorMessage) 5
)5 6
{ 
return 

BadRequest 
( 
Envelope "
." #
Error# (
(( )
errorMessage) 5
)5 6
)6 7
;7 8
} 
	protected 
IActionResult 

FromResult &
(& '
Result' -
result. 4
)4 5
{ 
return 
result 
. 
	IsSuccess 
?  !
Ok" $
($ %
)% &
:' (
Error) .
(. /
result/ 5
.5 6
Error6 ;
); <
;< =
} 
} ¡3
FC:\Users\fiyaz\Desktop\SJI3\SJI3.API\Controllers\TaskUnitController.cs
	namespace 	
SJI3
 
. 
API 
. 
Controllers 
; 
[ 
Route 
( 
$str 
) 
]  
[ 
ApiController 
] 
public 
class 
TaskUnitController 
:  !
BaseController" 0
{ 
private 
readonly 
	IMediator 
	_mediator (
;( )
public 

TaskUnitController 
( 
	IMediator '
mediator( 0
)0 1
{ 
	_mediator 
= 
mediator 
; 
} 
[ 
HttpGet 
( 
$str 
, 
Name 
= 
$str &
)& '
]' (
[ #
ExposePaginationHeaders 
] 
public 

async 
Task 
< 
IActionResult #
># $
Get% (
(( )
[) *
	FromQuery* 3
]3 4
ResourceParameters5 G
resourceParametersH Z
)Z [
{ 
var 
response 
= 
await 
	_mediator &
. 
CreateRequestClient  
<  !
IGetTaskUnitsQuery! 3
>3 4
(4 5
)5 6
. 
GetResponse 
<  
GetTaskUnitsResponse -
>- .
(. /
new/ 2
{ 
ResourceParameters   "
=  # $
resourceParameters  % 7
}!! 
)!! 
;!! 
HttpContext## 
.$$ 
Response$$ 
.%% 
Headers%% 
.&& 
Add&& 
(&& 
$str&& 
,&&  
JsonSerializer&&! /
.&&/ 0
	Serialize&&0 9
(&&9 :
new&&: =
{'' 
response(( 
.(( 
Message((  
.((  !
PaginationMetadata((! 3
.((3 4

TotalCount((4 >
,((> ?
response)) 
.)) 
Message))  
.))  !
PaginationMetadata))! 3
.))3 4
PageSize))4 <
,))< =
response** 
.** 
Message**  
.**  !
PaginationMetadata**! 3
.**3 4

TotalPages**4 >
,**> ?
response++ 
.++ 
Message++  
.++  !
PaginationMetadata++! 3
.++3 4
CurrentPage++4 ?
,++? @
response,, 
.,, 
Message,,  
.,,  !
PaginationMetadata,,! 3
.,,3 4
HasNext,,4 ;
,,,; <
response-- 
.-- 
Message--  
.--  !
PaginationMetadata--! 3
.--3 4
HasPrevious--4 ?
}.. 
,.. 
new// !
JsonSerializerOptions// )
{00  
PropertyNamingPolicy11 (
=11) *
JsonNamingPolicy11+ ;
.11; <
	CamelCase11< E
}22 
)22 
)22 
;22 
return44 
Ok44 
(44 
response44 
.44 
Message44 "
.44" #
	TaskUnits44# ,
.44, -
	ShapeData44- 6
(446 7
resourceParameters447 I
.44I J
Fields44J P
)44P Q
)44Q R
;44R S
}55 
[77 
HttpPost77 
(77 
$str77 
,77 
Name77 
=77 
$str77 '
)77' (
]77( )
public88 

async88 
Task88 
<88 
IActionResult88 #
>88# $
Post88% )
(88) *
[88* +
FromBody88+ 3
]883 4
PostTaskUnitCommand885 H
taskUnit88I Q
)88Q R
{99 
var:: 
response:: 
=:: 
await:: 
	_mediator:: &
.;; 
CreateRequestClient;;  
<;;  !
PostTaskUnitCommand;;! 4
>;;4 5
(;;5 6
);;6 7
.<< 
GetResponse<< 
<<< !
IPostTaskUnitResponse<< .
><<. /
(<</ 0
taskUnit<<0 8
)<<8 9
;<<9 :
return>> 
Ok>> 
(>> 
response>> 
.>> 
Message>> "
.>>" #
IsAdded>># *
)>>* +
;>>+ ,
}?? 
[AA 
HttpPutAA 
(AA 
$strAA -
)AA- .
]AA. /
publicBB 

asyncBB 
TaskBB 
<BB 
IActionResultBB #
>BB# $ 
UpdateTaskUnitStatusBB% 9
(BB9 :
GuidBB: >
idBB? A
,BBA B
[BBC D
FromBodyBBD L
]BBL M
PutTaskUnitCommandBBN `
taskUnitBBa i
)BBi j
{CC 
varDD 
existsDD 
=DD 
awaitDD 
	_mediatorDD $
.EE 
CreateRequestClientEE  
<EE  ! 
ITaskUnitExistsQueryEE! 5
>EE5 6
(EE6 7
)EE7 8
.FF 
GetResponseFF 
<FF "
TaskUnitExistsResponseFF /
>FF/ 0
(FF0 1
newFF1 4
{GG 
IdHH 
=HH 
idHH 
}II 
)II 
;II 
ifKK 

(KK 
!KK 
existsKK 
.KK 
MessageKK 
.KK 
ExistsKK "
)KK" #
returnLL 
ErrorLL 
(LL 
$"LL 
$strLL <
{LL< =
idLL= ?
}LL? @
"LL@ A
)LLA B
;LLB C
varNN 
responseNN 
=NN 
awaitNN 
	_mediatorNN &
.OO 
CreateRequestClientOO  
<OO  !
PutTaskUnitCommandOO! 3
>OO3 4
(OO4 5
)OO5 6
.PP 
GetResponsePP 
<PP  
IPutTaskUnitResponsePP -
>PP- .
(PP. /
taskUnitPP/ 7
)PP7 8
;PP8 9
returnRR 
OkRR 
(RR 
responseRR 
.RR 
MessageRR "
.RR" #
	IsUpdatedRR# ,
)RR, -
;RR- .
}SS 
}TT …
SC:\Users\fiyaz\Desktop\SJI3\SJI3.API\Decorators\ExposePaginationHeadersAttribute.cs
	namespace 	
SJI3
 
. 
API 
. 

Decorators 
; 
public 
class ,
 ExposePaginationHeadersAttribute -
:. /!
ResultFilterAttribute0 E
{ 
public 

override 
void 
OnResultExecuting *
(* +"
ResultExecutingContext+ A
contextB I
)I J
{ 
context		 
.		 
HttpContext		 
.		 
Response		 $
.		$ %
Headers		% ,
.		, -
Add		- 0
(		0 1
$str		1 P
,		P Q
$str		R `
)		` a
;		a b
base

 
.

 
OnResultExecuting

 
(

 
context

 &
)

& '
;

' (
} 
} á}
/C:\Users\fiyaz\Desktop\SJI3\SJI3.API\Program.cs
const 
string 
allowAllOrigins 
= 
$str 0
;0 1
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder!! 
.!! 
WebHost!! 
.!! 

UseKestrel!! 
(!! 
)!! 
;!! 
builder"" 
."" 
WebHost"" 
."" 
UseContentRoot"" 
("" 
	Directory"" (
.""( )
GetCurrentDirectory"") <
(""< =
)""= >
)""> ?
;""? @
builder## 
.## 
WebHost## 
.## 
UseIISIntegration## !
(##! "
)##" #
;### $
builder%% 
.%% 
Host%% 
.%% 

UseSerilog%% 
(%% 
(%% 
_%% 
,%% 
conf%%  
)%%  !
=>%%" $
{&& 
conf'' 
.(( 	
MinimumLevel((	 
.(( 
Debug(( 
((( 
)(( 
.)) 	
MinimumLevel))	 
.)) 
Override)) 
()) 
$str)) *
,))* +
LogEventLevel)), 9
.))9 :
Information)): E
)))E F
.** 	
Enrich**	 
.** 
FromLogContext** 
(** 
)**  
.++ 	
WriteTo++	 
.++ 
Console++ 
(++ 
)++ 
;++ 
},, 
),, 
;,, 
builder.. 
... 
Services.. 
... "
AddHttpContextAccessor.. '
(..' (
)..( )
;..) *
builder// 
.// 
Services// 
.// #
AddEndpointsApiExplorer// (
(//( )
)//) *
;//* +
builder00 
.00 
Services00 
.00 
AddSwaggerGen00 
(00 
opt00 "
=>00# %
{11 
opt22 
.22 
CustomSchemaIds22 
(22 
x22 
=>22 
x22 
.22 
FullName22 '
)22' (
;22( )
}33 
)33 
;33 
builder55 
.55 
Services55 
.55 
AddCors55 
(55 
options55  
=>55! #
{66 
options77 
.77 
	AddPolicy77 
(77 
allowAllOrigins77 %
,77% &
policy77' -
=>77. 0
{88 
policy99 
.99 
WithOrigins99 
(99 
$str99 2
)992 3
.:: 
AllowAnyHeader:: 
(:: 
):: 
.;; 
AllowAnyMethod;; 
(;; 
);; 
;;; 
}<< 
)<< 
;<< 
}== 
)== 
;== 
builder?? 
.?? 
Services?? 
.@@ 
AddControllers@@ 
(@@ 
)@@ 
.AA 
AddJsonOptionsAA 
(AA 
optAA 
=>AA 
{BB 
optCC 
.CC !
JsonSerializerOptionsCC !
.CC! " 
PropertyNamingPolicyCC" 6
=CC7 8
JsonNamingPolicyCC9 I
.CCI J
	CamelCaseCCJ S
;CCS T
optDD 
.DD !
JsonSerializerOptionsDD !
.DD! "
DictionaryKeyPolicyDD" 5
=DD6 7
JsonNamingPolicyDD8 H
.DDH I
	CamelCaseDDI R
;DDR S
optEE 
.EE !
JsonSerializerOptionsEE !
.EE! " 
ConfigureForNodaTimeEE" 6
(EE6 7!
DateTimeZoneProvidersEE7 L
.EEL M
TzdbEEM Q
)EEQ R
;EER S
}FF 
)FF 
;FF 
builderHH 
.HH 
ServicesHH 
.HH %
AddValidatorsFromAssemblyHH *
(HH* +
	AppDomainHH+ 4
.HH4 5
CurrentDomainHH5 B
.HHB C
LoadHHC G
(HHG H
$strHHH S
)HHS T
)HHT U
;HHU V
builderII 
.II 
ServicesII 
.II -
!AddFluentValidationAutoValidationII 2
(II2 3
)II3 4
.II4 51
%AddFluentValidationClientsideAdaptersII5 Z
(IIZ [
)II[ \
;II\ ]
varKK 
configKK 

=KK 
TypeAdapterConfigKK 
.KK 
GlobalSettingsKK -
;KK- .
configLL 
.LL 
ScanLL 
(LL 
	AppDomainLL 
.LL 
CurrentDomainLL #
.LL# $
LoadLL$ (
(LL( )
$strLL) 4
)LL4 5
)LL5 6
;LL6 7
builderMM 
.MM 
ServicesMM 
.MM 
AddSingletonMM 
(MM 
configMM $
)MM$ %
;MM% &
builderNN 
.NN 
ServicesNN 
.NN 
	AddScopedNN 
<NN 
IMapperNN "
,NN" #
ServiceMapperNN$ 1
>NN1 2
(NN2 3
)NN3 4
;NN4 5
builderPP 
.PP 
ServicesPP 
.PP 
AddMediatorPP 
(PP 
cfgPP  
=>PP! #
{QQ 
cfgRR 
.RR 
AddConsumersRR 
(RR 
	AppDomainRR 
.RR 
CurrentDomainRR ,
.RR, -
LoadRR- 1
(RR1 2
$strRR2 =
)RR= >
)RR> ?
;RR? @
}SS 
)SS 
;SS 
builderUU 
.UU 
ServicesUU 
.UU 
AddMassTransitUU 
(UU  
cfgUU  #
=>UU$ &
{VV 
cfgWW 
.WW -
!SetKebabCaseEndpointNameFormatterWW )
(WW) *
)WW* +
;WW+ ,
cfgXX 
.XX 
AddConsumersXX 
(XX 
	AppDomainXX 
.XX 
CurrentDomainXX ,
.XX, -
LoadXX- 1
(XX1 2
$strXX2 G
)XXG H
)XXH I
;XXI J
cfgYY 
.YY 
UsingInMemoryYY 
(YY 
(YY 
contextYY 
,YY 
configuratorYY  ,
)YY, -
=>YY. 0
{ZZ 
configurator[[ 
.[[ 
ConfigureEndpoints[[ '
([[' (
context[[( /
)[[/ 0
;[[0 1
}\\ 
)\\ 
;\\ 
}]] 
)]] 
;]] 
builder__ 
.__ 
Services__ 
.__ 
Scan__ 
(__ 
scan__ 
=>__ 
scan__ "
.__" #
FromAssemblies__# 1
(__1 2
	AppDomain__2 ;
.__; <
CurrentDomain__< I
.__I J
Load__J N
(__N O
$str__O Z
)__Z [
)__[ \
.`` 

AddClasses`` 
(`` 
classes`` 
=>`` 
classes`` "
.``" #
Where``# (
(``( )
type``) -
=>``. 0
type``1 5
.``5 6
Name``6 :
.``: ;
EndsWith``; C
(``C D
$str``D O
)``O P
)``P Q
)``Q R
.aa #
AsImplementedInterfacesaa 
(aa 
)aa 
.bb !
WithTransientLifetimebb 
(bb 
)bb 
)bb 
;bb 
builderdd 
.dd 
Servicesdd 
.dd 
Scandd 
(dd 
scandd 
=>dd 
scandd "
.dd" #
FromAssembliesdd# 1
(dd1 2
	AppDomaindd2 ;
.dd; <
CurrentDomaindd< I
.ddI J
LoadddJ N
(ddN O
$strddO d
)ddd e
)dde f
.ee 

AddClassesee 
(ee 
classesee 
=>ee 
classesee "
.ee" #
Whereee# (
(ee( )
typeee) -
=>ee. 0
typeee1 5
.ee5 6
Nameee6 :
.ee: ;
EndsWithee; C
(eeC D
$streeD P
)eeP Q
)eeQ R
)eeR S
.ff #
AsImplementedInterfacesff 
(ff 
)ff 
.gg !
WithTransientLifetimegg 
(gg 
)gg 
)gg 
;gg 
builderii 
.ii 
Servicesii 
.jj 
AddDbContextjj 
<jj 
AppDbContextjj 
>jj 
(jj  
optsjj  $
=>jj% '
{kk 
optsll 
.mm 
	UseNpgsqlmm 
(mm 
buildernn 
.nn 
Configurationnn %
.nn% &
GetConnectionStringnn& 9
(nn9 :
$strnn: M
)nnM N
,nnN O
optionsBuilderoo 
=>oo !
{pp 
optionsBuilderqq "
.qq" #
UseNodaTimeqq# .
(qq. /
)qq/ 0
;qq0 1
}rr 
)rr 
.ss &
EnableSensitiveDataLoggingss '
(ss' (
)ss( )
;ss) *
}tt 
)tt 
.uu 
	AddScopeduu 
<uu 
IUnitOfWorkuu 
,uu 

UnitOfWorkuu &
<uu& '
AppDbContextuu' 3
>uu3 4
>uu4 5
(uu5 6
)uu6 7
;uu7 8
builderww 
.ww 
Servicesww 
.ww 
AddTransientww 
(ww 
typeofww $
(ww$ %

IAppLoggerww% /
<ww/ 0
>ww0 1
)ww1 2
,ww2 3
typeofww4 :
(ww: ;
LoggerAdapterww; H
<wwH I
>wwI J
)wwJ K
)wwK L
;wwL M
builderxx 
.xx 
Servicesxx 
.xx 
AddTransientxx 
<xx 
ITypeHelperServicexx 0
,xx0 1
TypeHelperServicexx2 C
>xxC D
(xxD E
)xxE F
;xxF G
builderzz 
.zz 
Serviceszz 
.zz 
AddSingletonzz 
<zz 
IJobFactoryzz )
,zz) *

JobFactoryzz+ 5
>zz5 6
(zz6 7
)zz7 8
;zz8 9
builder{{ 
.{{ 
Services{{ 
.{{ 
AddSingleton{{ 
<{{ 
ISchedulerFactory{{ /
,{{/ 0
StdSchedulerFactory{{1 D
>{{D E
({{E F
){{F G
;{{G H
builder|| 
.|| 
Services|| 
.|| 
AddTransient|| 
<|| "
ITaskProcessingService|| 4
,||4 5!
TaskProcessingService||6 K
>||K L
(||L M
)||M N
;||N O
builder~~ 
.~~ 
Services~~ 
.~~ 
AddHostedService~~ !
<~~! "!
SeedBackgroundService~~" 7
>~~7 8
(~~8 9
)~~9 :
;~~: ;
builder 
. 
Services 
. 
AddHostedService !
<! "+
TaskProcessingBackgroundService" A
>A B
(B C
)C D
;D E
builderÅÅ 
.
ÅÅ 
Services
ÅÅ 
.
ÅÅ 
AddRefitClient
ÅÅ 
<
ÅÅ  
IDemoClientApi
ÅÅ  .
>
ÅÅ. /
(
ÅÅ/ 0
)
ÅÅ0 1
.
ÇÇ !
ConfigureHttpClient
ÇÇ 
(
ÇÇ 
c
ÇÇ 
=>
ÇÇ 
c
ÇÇ 
.
ÇÇ  
BaseAddress
ÇÇ  +
=
ÇÇ, -
new
ÇÇ. 1
Uri
ÇÇ2 5
(
ÇÇ5 6
builder
ÉÉ 
.
ÉÉ 
Configuration
ÉÉ 
.
ÉÉ 

GetSection
ÉÉ (
(
ÉÉ( )
nameof
ÉÉ) /
(
ÉÉ/ 0*
IntegrationApisConfiguration
ÉÉ0 L
)
ÉÉL M
)
ÉÉM N
.
ÑÑ 
Get
ÑÑ 
<
ÑÑ *
IntegrationApisConfiguration
ÑÑ -
>
ÑÑ- .
(
ÑÑ. /
)
ÑÑ/ 0
.
ÖÖ #
DemoClientBaseAddress
ÖÖ "
)
ÖÖ" #
)
ÖÖ# $
;
ÖÖ$ %
builderáá 
.
áá 
Services
áá 
.
áá 
AddSingleton
áá 
<
áá 

ITaskQueue
áá (
<
áá( )
Guid
áá) -
>
áá- .
>
áá. /
(
áá/ 0
_
áá0 1
=>
áá2 4
{àà 
if
ââ 
(
ââ 
!
ââ 	
int
ââ	 
.
ââ 
TryParse
ââ 
(
ââ 
builder
ââ 
.
ââ 
Configuration
ââ +
[
ââ+ ,
$str
ââ, ;
]
ââ; <
,
ââ< =
out
ââ> A
var
ââB E
queueCapacity
ââF S
)
ââS T
)
ââT U
queueCapacity
ää 
=
ää 
$num
ää 
;
ää 
return
ãã 

new
ãã !
BackgroundTaskQueue
ãã "
(
ãã" #
queueCapacity
ãã# 0
)
ãã0 1
;
ãã1 2
}åå 
)
åå 
;
åå 
builderéé 
.
éé 
Services
éé 
.
éé 

AddSignalR
éé 
(
éé 
)
éé 
;
éé 
varêê 
app
êê 
=
êê 	
builder
êê
 
.
êê 
Build
êê 
(
êê 
)
êê 
;
êê 
ifìì 
(
ìì 
app
ìì 
.
ìì 
Environment
ìì 
.
ìì 
IsDevelopment
ìì !
(
ìì! "
)
ìì" #
)
ìì# $
{îî 
app
ïï 
.
ïï '
UseDeveloperExceptionPage
ïï !
(
ïï! "
)
ïï" #
;
ïï# $
}ññ 
appòò 
.
òò 

UseSwagger
òò 
(
òò 
)
òò 
;
òò 
appôô 
.
ôô 
UseSwaggerUI
ôô 
(
ôô 
)
ôô 
;
ôô 
appõõ 
.
õõ 
UseMiddleware
õõ 
<
õõ 
ExceptionHandler
õõ "
>
õõ" #
(
õõ# $
)
õõ$ %
;
õõ% &
appùù 
.
ùù 
UseCors
ùù 
(
ùù 
allowAllOrigins
ùù 
)
ùù 
;
ùù 
appüü 
.
üü 

UseRouting
üü 
(
üü 
)
üü 
;
üü 
app†† 
.
†† 
UseAuthorization
†† 
(
†† 
)
†† 
;
†† 
app°° 
.
°° 
UseEndpoints
°° 
(
°° 
	endpoints
°° 
=>
°° 
{¢¢ 
	endpoints
££ 
.
££ 
MapControllers
££ 
(
££ 
)
££ 
;
££ 
	endpoints
§§ 
.
§§ 
MapHub
§§ 
<
§§ 
NotificationsHub
§§ %
>
§§% &
(
§§& '
$str
§§' =
)
§§= >
;
§§> ?
}•• 
)
•• 
;
•• 
tryßß 
{®® 
Log
©© 
.
©© 
Information
©© 
(
©© 
$str
©© !
)
©©! "
;
©©" #
app
™™ 
.
™™ 
Run
™™ 
(
™™ 
)
™™ 
;
™™ 
}´´ 
catch¨¨ 
(
¨¨ 
	Exception
¨¨ 
ex
¨¨ 
)
¨¨ 
{≠≠ 
Log
ÆÆ 
.
ÆÆ 
Fatal
ÆÆ 
(
ÆÆ 
ex
ÆÆ 
,
ÆÆ 
$str
ÆÆ /
)
ÆÆ/ 0
;
ÆÆ0 1
}ØØ 
finally∞∞ 
{±± 
Log
≤≤ 
.
≤≤ 
CloseAndFlush
≤≤ 
(
≤≤ 
)
≤≤ 
;
≤≤ 
}≥≥ À
=C:\Users\fiyaz\Desktop\SJI3\SJI3.API\ViewModels\LoginModel.cs
	namespace 	
SJI3
 
. 
API 
. 

ViewModels 
; 
public 
class 

LoginModel 
{ 
public 

string 
UserName 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
Password 
{ 
get  
;  !
set" %
;% &
}' (
} 