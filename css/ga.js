﻿(function(){var g=void 0,h=!0,i=null,j=!1,aa=encodeURIComponent,ba=Infinity,ca=setTimeout,da=decodeURIComponent,k=Math;function ea(a,b){return a.onload=b}function fa(a,b){return a.name=b}
var m="push",ga="slice",ha="replace",ia="load",ja="floor",ka="cookie",n="charAt",la="value",p="indexOf",ma="match",q="name",na="host",t="toString",u="length",v="prototype",pa="clientWidth",w="split",qa="stopPropagation",ra="scope",x="location",y="getString",sa="random",ta="clientHeight",ua="href",z="substring",va="navigator",A="join",C="toLowerCase",D;function wa(a,b){switch(b){case 0:return""+a;case 1:return 1*a;case 2:return!!a;case 3:return 1E3*a}return a}function E(a,b){return g==a||"-"==a&&!b||""==a}function xa(a){if(!a||""==a)return"";for(;a&&-1<" \n\r\t"[p](a[n](0));)a=a[z](1);for(;a&&-1<" \n\r\t"[p](a[n](a[u]-1));)a=a[z](0,a[u]-1);return a}function ya(a){var b=1,c=0,d;if(!E(a)){b=0;for(d=a[u]-1;0<=d;d--)c=a.charCodeAt(d),b=(b<<6&268435455)+c+(c<<14),c=b&266338304,b=0!=c?b^c>>21:b}return b}
function za(){return k.round(2147483647*k[sa]())}function Aa(){}function Ba(a,b){if(aa instanceof Function)return b?encodeURI(a):aa(a);F(68);return escape(a)}function G(a){a=a[w]("+")[A](" ");if(da instanceof Function)try{return da(a)}catch(b){F(17)}else F(68);return unescape(a)}
var Ca=function(a,b,c,d){a.addEventListener?a.addEventListener(b,c,!!d):a.attachEvent&&a.attachEvent("on"+b,c)},Da=function(a,b,c,d){a.removeEventListener?a.removeEventListener(b,c,!!d):a.detachEvent&&a.detachEvent("on"+b,c)};function H(a){return a&&0<a[u]?a[0]:""}function Ea(a){var b=a?a[u]:0;return 0<b?a[b-1]:""}var Fa=function(){this.prefix="ga.";this.I={}};Fa[v].set=function(a,b){this.I[this.prefix+a]=b};Fa[v].get=function(a){return this.I[this.prefix+a]};
Fa[v].contains=function(a){return this.get(a)!==g};function Ga(a){0==a[p]("www.")&&(a=a[z](4));return a[C]()}function Ha(a,b){var c,d={url:a,protocol:"http",host:"",path:"",c:new Fa,anchor:""};if(!a)return d;c=a[p]("://");0<=c&&(d.protocol=a[z](0,c),a=a[z](c+3));c=a.search("/|\\?|#");if(0<=c)d.host=a[z](0,c)[C](),a=a[z](c);else return d.host=a[C](),d;c=a[p]("#");0<=c&&(d.anchor=a[z](c+1),a=a[z](0,c));c=a[p]("?");0<=c&&(Ia(d.c,a[z](c+1)),a=a[z](0,c));d.anchor&&b&&Ia(d.c,d.anchor);a&&"/"==a[n](0)&&(a=a[z](1));d.path=a;return d}
function Ia(a,b){function c(b,c){a.contains(b)||a.set(b,[]);a.get(b)[m](c)}for(var d=xa(b)[w]("&"),e=0;e<d[u];e++)if(d[e]){var f=d[e][p]("=");0>f?c(d[e],"1"):c(d[e][z](0,f),d[e][z](f+1))}}function Ja(a,b){if(E(a)||"["==a[n](0)&&"]"==a[n](a[u]-1))return"-";var c=I.domain;return a[p](c+(b&&"/"!=b?b:""))==(0==a[p]("http://")?7:0==a[p]("https://")?8:0)?"0":a};function Ka(a,b,c){1<=100*k[sa]()||(a=["utmt=error","utmerr="+a,"utmwv=5.2.3","utmn="+za(),"utmsp=1"],b&&a[m]("api="+b),c&&a[m]("msg="+Ba(c[z](0,100))),J.q&&a[m]("aip=1"),La(a[A]("&")))};var Ma=0;function K(a){return(a?"_":"")+Ma++}
var Na=K(),Oa=K(),Pa=K(),Qa=K(),Ra=K(),L=K(),M=K(),Sa=K(),Ta=K(),Ua=K(),Va=K(),Wa=K(),Xa=K(),Ya=K(),Za=K(),$a=K(),ab=K(),bb=K(),cb=K(),db=K(),eb=K(),fb=K(),gb=K(),hb=K(),ib=K(),jb=K(),kb=K(),lb=K(),mb=K(),nb=K(),ob=K(),pb=K(),qb=K(),rb=K(),sb=K(),N=K(h),tb=K(),ub=K(),vb=K(),wb=K(),xb=K(),yb=K(),zb=K(),Ab=K(),Bb=K(),Cb=K(),O=K(h),Db=K(h),Eb=K(h),Gb=K(h),Hb=K(h),Ib=K(h),Jb=K(h),Kb=K(h),Lb=K(h),Mb=K(h),Nb=K(h),P=K(h),Ob=K(h),Pb=K(h),Qb=K(h),Rb=K(h),Sb=K(h),Tb=K(h),Ub=K(h),Vb=K(h),Wb=K(h),Xb=K(h),Yb=
K(h),Zb=K(h),$b=K(h),ac=K(),bc=K(),cc=K();K();var dc=K(),ec=K(),fc=K(),gc=K(),hc=K(),ic=K(),jc=K(),kc=K(),lc=K(),pc=K();K();var qc=K(),rc=K(),sc=K();var tc=function(){function a(a,c,d){Q(R[v],a,c,d)}S("_getName",Pa,58);S("_getAccount",Na,64);S("_visitCode",O,54);S("_getClientInfo",Ya,53,1);S("_getDetectTitle",ab,56,1);S("_getDetectFlash",Za,65,1);S("_getLocalGifPath",kb,57);S("_getServiceMode",lb,59);T("_setClientInfo",Ya,66,2);T("_setAccount",Na,3);T("_setNamespace",Oa,48);T("_setAllowLinker",Va,11,2);T("_setDetectFlash",Za,61,2);T("_setDetectTitle",ab,62,2);T("_setLocalGifPath",kb,46,0);T("_setLocalServerMode",lb,92,g,0);T("_setRemoteServerMode",
lb,63,g,1);T("_setLocalRemoteServerMode",lb,47,g,2);T("_setSampleRate",jb,45,1);T("_setCampaignTrack",$a,36,2);T("_setAllowAnchor",Wa,7,2);T("_setCampNameKey",cb,41);T("_setCampContentKey",hb,38);T("_setCampIdKey",bb,39);T("_setCampMediumKey",fb,40);T("_setCampNOKey",ib,42);T("_setCampSourceKey",eb,43);T("_setCampTermKey",gb,44);T("_setCampCIdKey",db,37);T("_setCookiePath",M,9,0);T("_setMaxCustomVariables",mb,0,1);T("_setVisitorCookieTimeout",Sa,28,1);T("_setSessionCookieTimeout",Ta,26,1);T("_setCampaignCookieTimeout",
Ua,29,1);T("_setReferrerOverride",vb,49);T("_setSiteSpeedSampleRate",lc,132);a("_trackPageview",R[v].na,1);a("_trackEvent",R[v].v,4);a("_trackPageLoadTime",R[v].ma,100);a("_trackSocial",R[v].oa,104);a("_trackTrans",R[v].pa,18);a("_sendXEvent",R[v].u,78);a("_createEventTracker",R[v].V,74);a("_getVersion",R[v].$,60);a("_setDomainName",R[v].t,6);a("_setAllowHash",R[v].ea,8);a("_getLinkerUrl",R[v].Z,52);a("_link",R[v].link,101);a("_linkByPost",R[v].da,102);a("_setTrans",R[v].ha,20);a("_addTrans",R[v].O,
21);a("_addItem",R[v].M,19);a("_setTransactionDelim",R[v].ia,82);a("_setCustomVar",R[v].fa,10);a("_deleteCustomVar",R[v].X,35);a("_getVisitorCustomVar",R[v].aa,50);a("_setXKey",R[v].ka,83);a("_setXValue",R[v].la,84);a("_getXKey",R[v].ba,76);a("_getXValue",R[v].ca,77);a("_clearXKey",R[v].S,72);a("_clearXValue",R[v].T,73);a("_createXObj",R[v].W,75);a("_addIgnoredOrganic",R[v].K,15);a("_clearIgnoredOrganic",R[v].P,97);a("_addIgnoredRef",R[v].L,31);a("_clearIgnoredRef",R[v].Q,32);a("_addOrganic",R[v].N,
14);a("_clearOrganic",R[v].R,70);a("_cookiePathCopy",R[v].U,30);a("_get",R[v].Y,106);a("_set",R[v].ga,107);a("_addEventListener",R[v].addEventListener,108);a("_removeEventListener",R[v].removeEventListener,109);a("_initData",R[v].m,2);a("_setVar",R[v].ja,22);T("_setSessionTimeout",Ta,27,3);T("_setCookieTimeout",Ua,25,3);T("_setCookiePersistence",Sa,24,1);a("_setAutoTrackOutbound",Aa,79);a("_setTrackOutboundSubdomains",Aa,81);a("_setHrefExamineLimit",Aa,80)},Q=function(a,b,c,d){a[b]=function(){try{return F(d),
c.apply(this,arguments)}catch(a){throw Ka("exc",b,a&&a[q]),a;}}},S=function(a,b,c,d){R[v][a]=function(){try{return F(c),wa(this.a.get(b),d)}catch(e){throw Ka("exc",a,e&&e[q]),e;}}},T=function(a,b,c,d,e){R[v][a]=function(f){try{F(c),e==g?this.a.set(b,wa(f,d)):this.a.set(b,e)}catch(l){throw Ka("exc",a,l&&l[q]),l;}}},uc=function(a,b){return{type:b,target:a,stopPropagation:function(){throw"aborted";}}};var vc=function(a,b){return"/"!==b?j:(0==a[p]("www.google.")||0==a[p](".google.")||0==a[p]("google."))&&!(-1<a[p]("google.org"))?h:j},wc=function(a){var b=a.get(Ra),c=a[y](M,"/");vc(b,c)&&a[qa]()};var Bc=function(){var a={},b={},c=new xc;this.g=function(a,b){c.add(a,b)};var d=new xc;this.d=function(a,b){d.add(a,b)};var e=j,f=j,l=h;this.J=function(){e=h};this.f=function(a){this[ia]();this.set(ac,a,h);a=new yc(this);e=j;d.execute(this);e=h;b={};this.i();a.qa()};this.load=function(){e&&(e=j,this.sa(),zc(this),f||(f=h,c.execute(this),Ac(this),zc(this)),e=h)};this.i=function(){if(e)if(f)e=j,Ac(this),e=h;else this[ia]()};this.get=function(c){c&&"_"==c[n](0)&&this[ia]();return b[c]!==g?b[c]:a[c]};
this.set=function(c,d,e){c&&"_"==c[n](0)&&this[ia]();e?b[c]=d:a[c]=d;c&&"_"==c[n](0)&&this.i()};this.n=function(b){a[b]=this.b(b,0)+1};this.b=function(a,b){var c=this.get(a);return c==g||""===c?b:1*c};this.getString=function(a,b){var c=this.get(a);return c==g?b:c+""};this.sa=function(){if(l){var b=this[y](Ra,""),c=this[y](M,"/");vc(b,c)||(a[L]=a[Xa]&&""!=b?ya(b):1,l=j)}}};Bc[v].stopPropagation=function(){throw"aborted";};
var yc=function(a){var b=this;this.j=0;var c=a.get(bc);this.Aa=function(){0<b.j&&c&&(b.j--,b.j||c())};this.qa=function(){!b.j&&c&&ca(c,10)};a.set(cc,b,h)};function Cc(a,b){for(var b=b||[],c=0;c<b[u];c++){var d=b[c];if(""+a==d||0==d[p](a+"."))return d}return"-"}
var Ec=function(a,b,c){c=c?"":a[y](L,"1");b=b[w](".");if(6!==b[u]||Dc(b[0],c))return j;var c=1*b[1],d=1*b[2],e=1*b[3],f=1*b[4],b=1*b[5];if(!(0<=c&&0<d&&0<e&&0<f&&0<=b))return F(110),j;a.set(O,c);a.set(Hb,d);a.set(Ib,e);a.set(Jb,f);a.set(Kb,b);return h},Fc=function(a){var b=a.get(O),c=a.get(Hb),d=a.get(Ib),e=a.get(Jb),f=a.b(Kb,1);b==g?F(113):NaN==b&&F(114);0<=b&&0<c&&0<d&&0<e&&0<=f||F(115);return[a.b(L,1),b!=g?b:"-",c||"-",d||"-",e||"-",f][A](".")},Gc=function(a){return[a.b(L,1),a.b(Nb,0),a.b(P,1),
a.b(Ob,0)][A](".")},Hc=function(a,b,c){var c=c?"":a[y](L,"1"),d=b[w](".");if(4!==d[u]||Dc(d[0],c))d=i;a.set(Nb,d?1*d[1]:0);a.set(P,d?1*d[2]:10);a.set(Ob,d?1*d[3]:a.get(Qa));return d!=i||!Dc(b,c)},Ic=function(a,b){var c=Ba(a[y](Eb,"")),d=[],e=a.get(N);if(!b&&e){for(var f=0;f<e[u];f++){var l=e[f];l&&1==l[ra]&&d[m](f+"="+Ba(l[q])+"="+Ba(l[la])+"=1")}0<d[u]&&(c+="|"+d[A]("^"))}return c?a.b(L,1)+"."+c:i},Jc=function(a,b,c){c=c?"":a[y](L,"1");b=b[w](".");if(2>b[u]||Dc(b[0],c))return j;b=b[ga](1)[A](".")[w]("|");
0<b[u]&&a.set(Eb,G(b[0]));if(1>=b[u])return h;for(var c=b[1][w](-1==b[1][p](",")?"^":","),d=0;d<c[u];d++){var e=c[d][w]("=");if(4==e[u]){var f={};fa(f,G(e[1]));f.value=G(e[2]);f.scope=1;a.get(N)[e[0]]=f}}0<=b[1][p]("^")&&F(125);return h},Lc=function(a,b){var c=Kc(a,b);return c?[a.b(L,1),a.b(Pb,0),a.b(Qb,1),a.b(Rb,1),c][A]("."):""},Kc=function(a){function b(b,e){if(!E(a.get(b))){var f=a[y](b,""),f=f[w](" ")[A]("%20"),f=f[w]("+")[A]("%20");c[m](e+"="+f)}}var c=[];b(Tb,"utmcid");b(Xb,"utmcsr");b(Vb,
"utmgclid");b(Wb,"utmdclid");b(Ub,"utmccn");b(Yb,"utmcmd");b(Zb,"utmctr");b($b,"utmcct");return c[A]("|")},Nc=function(a,b,c){c=c?"":a[y](L,"1");b=b[w](".");if(5>b[u]||Dc(b[0],c))return a.set(Pb,g),a.set(Qb,g),a.set(Rb,g),a.set(Tb,g),a.set(Ub,g),a.set(Xb,g),a.set(Yb,g),a.set(Zb,g),a.set($b,g),a.set(Vb,g),a.set(Wb,g),j;a.set(Pb,1*b[1]);a.set(Qb,1*b[2]);a.set(Rb,1*b[3]);Mc(a,b[ga](4)[A]("."));return h},Mc=function(a,b){function c(a){return(a=b[ma](a+"=(.*?)(?:\\|utm|$)"))&&2==a[u]?a[1]:g}function d(b,
c){c&&(c=e?G(c):c[w]("%20")[A](" "),a.set(b,c))}-1==b[p]("=")&&(b=G(b));var e="2"==c("utmcvr");d(Tb,c("utmcid"));d(Ub,c("utmccn"));d(Xb,c("utmcsr"));d(Yb,c("utmcmd"));d(Zb,c("utmctr"));d($b,c("utmcct"));d(Vb,c("utmgclid"));d(Wb,c("utmdclid"))},Dc=function(a,b){return b?a!=b:!/^\d+$/.test(a)};var xc=function(){this.s=[]};xc[v].add=function(a,b){this.s[m]({name:a,Ea:b})};xc[v].execute=function(a){try{for(var b=0;b<this.s[u];b++)this.s[b].Ea.call(U,a)}catch(c){}};function Oc(a){100!=a.get(jb)&&a.get(O)%1E4>=100*a.get(jb)&&a[qa]()}function Pc(a){Qc()&&a[qa]()}function Rc(a){"file:"==I[x].protocol&&a[qa]()}function Sc(a){a.get(ub)||a.set(ub,I.title,h);a.get(tb)||a.set(tb,I[x].pathname+I[x].search,h)};var Tc=new function(){var a=[];this.set=function(b){a[b]=h};this.Fa=function(){for(var b=[],c=0;c<a[u];c++)a[c]&&(b[k[ja](c/6)]=b[k[ja](c/6)]^1<<c%6);for(c=0;c<b[u];c++)b[c]="ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_"[n](b[c]||0);return b[A]("")+"~"}};function F(a){Tc.set(a)};var U=window,I=document,Qc=function(){var a=U._gaUserPrefs;return a&&a.ioo&&a.ioo()},Uc=function(a,b){ca(a,b)},V=function(a){for(var b=[],c=I[ka][w](";"),a=RegExp("^\\s*"+a+"=\\s*(.*?)\\s*$"),d=0;d<c[u];d++){var e=c[d][ma](a);e&&b[m](e[1])}return b},W=function(a,b,c,d,e){var f;f=Qc()?j:vc(d,c)?j:h;if(f){if(b&&0<=U[va].userAgent[p]("Firefox")){b=b[ha](/\n|\r/g," ");f=0;for(var l=b[u];f<l;++f){var o=b.charCodeAt(f)&255;if(10==o||13==o)b=b[z](0,f)+"?"+b[z](f+1)}}b&&2E3<b[u]&&(b=b[z](0,2E3),F(69));a=
a+"="+b+"; path="+c+"; ";e&&(a+="expires="+(new Date((new Date).getTime()+e)).toGMTString()+"; ");d&&(a+="domain="+d+";");I.cookie=a}};var Vc,Wc,Xc=function(){if(!Vc){var a={},b=U[va],c=U.screen;a.H=c?c.width+"x"+c.height:"-";a.G=c?c.colorDepth+"-bit":"-";a.language=(b&&(b.language||b.browserLanguage)||"-")[C]();a.javaEnabled=b&&b.javaEnabled()?1:0;a.characterSet=I.characterSet||I.charset||"-";try{var d=I.documentElement,e=I.body,f=e&&e[pa]&&e[ta],b=[];d&&d[pa]&&d[ta]&&("CSS1Compat"===I.compatMode||!f)?b=[d[pa],d[ta]]:f&&(b=[e[pa],e[ta]]);a.Ba=b[A]("x")}catch(l){F(135)}Vc=a}},Yc=function(){Xc();for(var a=Vc,b=U[va],a=b.appName+b.version+
a.language+b.platform+b.userAgent+a.javaEnabled+a.H+a.G+(I[ka]?I[ka]:"")+(I.referrer?I.referrer:""),b=a[u],c=U.history[u];0<c;)a+=c--^b++;return ya(a)},Zc=function(a){Xc();var b=Vc;a.set(xb,b.H);a.set(yb,b.G);a.set(Bb,b.language);a.set(Cb,b.characterSet);a.set(zb,b.javaEnabled);a.set(sc,b.Ba);if(a.get(Ya)&&a.get(Za)){if(!(b=Wc)){var c,d,e;d="ShockwaveFlash";if((b=(b=U[va])?b.plugins:g)&&0<b[u])for(c=0;c<b[u]&&!e;c++)d=b[c],-1<d[q][p]("Shockwave Flash")&&(e=d.description[w]("Shockwave Flash ")[1]);
else{d=d+"."+d;try{c=new ActiveXObject(d+".7"),e=c.GetVariable("$version")}catch(f){}if(!e)try{c=new ActiveXObject(d+".6"),e="WIN 6,0,21,0",c.AllowScriptAccess="always",e=c.GetVariable("$version")}catch(l){}if(!e)try{c=new ActiveXObject(d),e=c.GetVariable("$version")}catch(o){}e&&(e=e[w](" ")[1][w](","),e=e[0]+"."+e[1]+" r"+e[2])}b=e?e:"-"}Wc=b;a.set(Ab,Wc)}else a.set(Ab,"-")};var X=function(){Q(X[v],"push",X[v][m],5);Q(X[v],"_createAsyncTracker",X[v].Ca,33);Q(X[v],"_getAsyncTracker",X[v].Da,34);this.r=0};X[v].Ca=function(a,b){return J.l(a,b||"")};X[v].Da=function(a){return J.p(a)};X[v].push=function(a){0<this.r&&F(105);this.r++;for(var b=arguments,c=0,d=0;d<b[u];d++)try{if("function"===typeof b[d])b[d]();else{var e="",f=b[d][0],l=f.lastIndexOf(".");0<l&&(e=f[z](0,l),f=f[z](l+1));var o="_gat"==e?J:"_gaq"==e?$c:J.p(e);o[f].apply(o,b[d][ga](1))}}catch(r){c++}this.r--;return c};var dd=function(){function a(a,b,c,d){g==f[a]&&(f[a]={});g==f[a][b]&&(f[a][b]=[]);f[a][b][c]=d}function b(a,b,c){if(g!=f[a]&&g!=f[a][b])return f[a][b][c]}function c(a,b){if(g!=f[a]&&g!=f[a][b]){f[a][b]=g;var c=h,d;for(d=0;d<l[u];d++)if(g!=f[a][l[d]]){c=j;break}c&&(f[a]=g)}}function d(a){var b="",c=j,d,e;for(d=0;d<l[u];d++)if(e=a[l[d]],g!=e){c&&(b+=l[d]);for(var c=[],f=g,Z=g,Z=0;Z<e[u];Z++)if(g!=e[Z]){f="";Z!=$&&g==e[Z-1]&&(f+=Z[t]()+oa);for(var cd=e[Z],mc="",Fb=g,nc=g,oc=g,Fb=0;Fb<cd[u];Fb++)nc=cd[n](Fb),
oc=B[nc],mc+=g!=oc?oc:nc;f+=mc;c[m](f)}b+=o+c[A](s)+r;c=j}else c=h;return b}var e=this,f=[],l=["k","v"],o="(",r=")",s="*",oa="!",B={"'":"'0"};B[r]="'1";B[s]="'2";B[oa]="'3";var $=1;e.va=function(a){return g!=f[a]};e.o=function(){for(var a="",b=0;b<f[u];b++)g!=f[b]&&(a+=b[t]()+d(f[b]));return a};e.ua=function(a){if(a==g)return e.o();for(var b=a.o(),c=0;c<f[u];c++)g!=f[c]&&!a.va(c)&&(b+=c[t]()+d(f[c]));return b};e.e=function(b,c,d){if(!ad(d))return j;a(b,"k",c,d);return h};e.k=function(b,c,d){if(!bd(d))return j;
a(b,"v",c,d[t]());return h};e.getKey=function(a,c){return b(a,"k",c)};e.C=function(a,c){return b(a,"v",c)};e.A=function(a){c(a,"k")};e.B=function(a){c(a,"v")};Q(e,"_setKey",e.e,89);Q(e,"_setValue",e.k,90);Q(e,"_getKey",e.getKey,87);Q(e,"_getValue",e.C,88);Q(e,"_clearKey",e.A,85);Q(e,"_clearValue",e.B,86)};function ad(a){return"string"==typeof a}function bd(a){return"number"!=typeof a&&(g==Number||!(a instanceof Number))||k.round(a)!=a||NaN==a||a==ba?j:h};var ed=function(a){var b=U.gaGlobal;a&&!b&&(U.gaGlobal=b={});return b},fd=function(){var a=ed(h).hid;a==i&&(a=za(),ed(h).hid=a);return a},gd=function(a){a.set(wb,fd());var b=ed();if(b&&b.dh==a.get(L)){var c=b.sid;c&&("0"==c&&F(112),a.set(Jb,c),a.get(Db)&&a.set(Ib,c));b=b.vid;a.get(Db)&&b&&(b=b[w]("."),1*b[1]||F(112),a.set(O,1*b[0]),a.set(Hb,1*b[1]))}};var hd,id=function(a,b,c){var d=a[y](Ra,""),e=a[y](M,"/"),a=a.b(Sa,0);W(b,c,e,d,a)},Ac=function(a){var b=a[y](Ra,"");a.b(L,1);var c=a[y](M,"/");W("__utma",Fc(a),c,b,a.get(Sa));W("__utmb",Gc(a),c,b,a.get(Ta));W("__utmc",""+a.b(L,1),c,b);var d=Lc(a,h);d?W("__utmz",d,c,b,a.get(Ua)):W("__utmz","",c,b,-1);(d=Ic(a,j))?W("__utmv",d,c,b,a.get(Sa)):W("__utmv","",c,b,-1)},zc=function(a){var b=a.b(L,1);if(!Ec(a,Cc(b,V("__utma"))))return a.set(Gb,h),j;var c=!Hc(a,Cc(b,V("__utmb")));a.set(Mb,c);Nc(a,Cc(b,V("__utmz")));
Jc(a,Cc(b,V("__utmv")));hd=!c;return h},jd=function(a){hd||0<V("__utmb")[u]||(W("__utmd","1",a[y](M,"/"),a[y](Ra,""),1E4),0==V("__utmd")[u]&&a[qa]())};var md=function(a){a.get(O)==g?kd(a):a.get(Gb)&&!a.get(qc)?kd(a):a.get(Mb)&&ld(a)},nd=function(a){a.get(Sb)&&!a.get(Lb)&&(ld(a),a.set(Qb,a.get(Kb)))},kd=function(a){var b=a.get(Qa);a.set(Db,h);a.set(O,za()^Yc(a)&2147483647);a.set(Eb,"");a.set(Hb,b);a.set(Ib,b);a.set(Jb,b);a.set(Kb,1);a.set(Lb,h);a.set(Nb,0);a.set(P,10);a.set(Ob,b);a.set(N,[]);a.set(Gb,j);a.set(Mb,j)},ld=function(a){a.set(Ib,a.get(Jb));a.set(Jb,a.get(Qa));a.n(Kb);a.set(Lb,h);a.set(Nb,0);a.set(P,10);a.set(Ob,a.get(Qa));a.set(Mb,j)};var od="daum:q,eniro:search_word,naver:query,pchome:q,images.google:q,google:q,yahoo:p,yahoo:q,msn:q,bing:q,aol:query,aol:q,lycos:query,ask:q,netscape:query,cnn:query,about:terms,mamma:q,voila:rdata,virgilio:qs,live:q,baidu:wd,alice:qs,yandex:text,najdi:q,seznam:q,search:q,wp:szukaj,onet:qt,yam:k,kvasir:q,ozu:q,terra:query,rambler:query".split(","),ud=function(a){if(a.get($a)&&!a.get(qc)){for(var b=!E(a.get(Tb))||!E(a.get(Xb))||!E(a.get(Vb))||!E(a.get(Wb)),c={},d=0;d<pd[u];d++){var e=pd[d];c[e]=a.get(e)}d=
Ha(I[x][ua],a.get(Wa));if(!("1"==Ea(d.c.get(a.get(ib)))&&b)&&(d=qd(a,d)||rd(a),!d&&!b&&a.get(Lb)&&(sd(a,g,"(direct)",g,g,"(direct)","(none)",g,g),d=h),d&&(a.set(Sb,td(a,c)),b="(direct)"==a.get(Xb)&&"(direct)"==a.get(Ub)&&"(none)"==a.get(Yb),a.get(Sb)||a.get(Lb)&&!b)))a.set(Pb,a.get(Qa)),a.set(Qb,a.get(Kb)),a.n(Rb)}},qd=function(a,b){function c(c,d){var d=d||"-",e=Ea(b.c.get(a.get(c)));return e&&"-"!=e?G(e):d}var d=Ea(b.c.get(a.get(bb)))||"-",e=Ea(b.c.get(a.get(eb)))||"-",f=Ea(b.c.get(a.get(db)))||
"-",l=Ea(b.c.get("dclid"))||"-",o=c(cb,"(not set)"),r=c(fb,"(not set)"),s=c(gb),oa=c(hb);if(E(d)&&E(f)&&E(l)&&E(e))return j;if(E(s)){var B=Ja(a.get(vb),a.get(M)),B=Ha(B,h);(B=vd(a,B))&&!E(B[1]&&!B[2])&&(s=B[1])}sd(a,d,e,f,l,o,r,s,oa);return h},rd=function(a){var b=Ja(a.get(vb),a.get(M)),c=Ha(b,h);if(!(b!=g&&b!=i&&""!=b&&"0"!=b&&"-"!=b&&0<=b[p]("://"))||c&&-1<c[na][p]("google")&&c.c.contains("q")&&"cse"==c.path)return j;if((b=vd(a,c))&&!b[2])return sd(a,g,b[0],g,g,"(organic)","organic",b[1],g),h;if(b)return j;
if(a.get(Lb))a:{for(var b=a.get(pb),d=Ga(c[na]),e=0;e<b[u];++e)if(-1<d[p](b[e])){a=j;break a}sd(a,g,d,g,g,"(referral)","referral",g,"/"+c.path);a=h}else a=j;return a},vd=function(a,b){for(var c=a.get(nb),d=0;d<c[u];++d){var e=c[d][w](":");if(-1<b[na][p](e[0][C]())){var f=b.c.get(e[1]);if(f&&(f=H(f),!f&&-1<b[na][p]("google.")&&(f="(not provided)"),!e[3]||-1<b.url[p](e[3]))){a:{for(var c=f,d=a.get(ob),c=G(c)[C](),l=0;l<d[u];++l)if(c==d[l]){c=h;break a}c=j}return[e[2]||e[0],f,c]}}}return i},sd=function(a,
b,c,d,e,f,l,o,r){a.set(Tb,b);a.set(Xb,c);a.set(Vb,d);a.set(Wb,e);a.set(Ub,f);a.set(Yb,l);a.set(Zb,o);a.set($b,r)},pd=[Ub,Tb,Vb,Wb,Xb,Yb,Zb,$b],td=function(a,b){function c(a){a=(""+a)[w]("+")[A]("%20");return a=a[w](" ")[A]("%20")}function d(c){var d=""+(a.get(c)||""),c=""+(b[c]||"");return 0<d[u]&&d==c}if(d(Vb)||d(Wb))return F(131),j;for(var e=0;e<pd[u];e++){var f=pd[e],l=b[f]||"-",f=a.get(f)||"-";if(c(l)!=c(f))return h}return j};var xd=function(a){wd(a,I[x][ua])?(a.set(qc,h),F(12)):a.set(qc,j)},wd=function(a,b){if(!a.get(Va))return j;var c=Ha(b,a.get(Wa)),d=H(c.c.get("__utma")),e=H(c.c.get("__utmb")),f=H(c.c.get("__utmc")),l=H(c.c.get("__utmx")),o=H(c.c.get("__utmz")),r=H(c.c.get("__utmv")),c=H(c.c.get("__utmk"));if(ya(""+d+e+f+l+o+r)!=c){d=G(d);e=G(e);f=G(f);l=G(l);a:{for(var f=d+e+f+l,s=0;3>s;s++){for(var oa=0;3>oa;oa++){if(c==ya(f+o+r)){F(127);c=[o,r];break a}var B=o[ha](/ /g,"%20"),$=r[ha](/ /g,"%20");if(c==ya(f+B+$)){F(128);
c=[B,$];break a}B=B[ha](/\+/g,"%20");$=$[ha](/\+/g,"%20");if(c==ya(f+B+$)){F(129);c=[B,$];break a}o=G(o)}r=G(r)}c=g}if(!c)return j;o=c[0];r=c[1]}if(!Ec(a,d,h))return j;Hc(a,e,h);Nc(a,o,h);Jc(a,r,h);yd(a,l,h);return h},Ad=function(a,b,c){var d;d=Fc(a)||"-";var e=Gc(a)||"-",f=""+a.b(L,1)||"-",l=zd(a)||"-",o=Lc(a,j)||"-",a=Ic(a,j)||"-",r=ya(""+d+e+f+l+o+a),s=[];s[m]("__utma="+d);s[m]("__utmb="+e);s[m]("__utmc="+f);s[m]("__utmx="+l);s[m]("__utmz="+o);s[m]("__utmv="+a);s[m]("__utmk="+r);d=s[A]("&");if(!d)return b;
e=b[p]("#");if(c)return 0>e?b+"#"+d:b+"&"+d;c="";f=b[p]("?");0<e&&(c=b[z](e),b=b[z](0,e));return 0>f?b+"?"+d+c:b+"&"+d+c};var Bd="|",Dd=function(a,b,c,d,e,f,l,o,r){var s=Cd(a,b);s||(s={},a.get(qb)[m](s));s.id_=b;s.affiliation_=c;s.total_=d;s.tax_=e;s.shipping_=f;s.city_=l;s.state_=o;s.country_=r;s.items_=s.items_||[];return s},Ed=function(a,b,c,d,e,f,l){var a=Cd(a,b)||Dd(a,b,"",0,0,0,"","",""),o;a:{if(a&&a.items_){o=a.items_;for(var r=0;r<o[u];r++)if(o[r].sku_==c){o=o[r];break a}}o=i}r=o||{};r.transId_=b;r.sku_=c;r.name_=d;r.category_=e;r.price_=f;r.quantity_=l;o||a.items_[m](r);return r},Cd=function(a,b){for(var c=
a.get(qb),d=0;d<c[u];d++)if(c[d].id_==b)return c[d];return i};var Fd,Gd=function(a){if(!Fd){var b,c=I[x].hash;b=U[q];var d=/^#?gaso=([^&]*)/;if(c=(b=(c=c&&c[ma](d)||b&&b[ma](d))?c[1]:H(V("GASO")))&&b[ma](/^(?:\|([-0-9a-z.]{1,40})\|)?([-.\w]{10,1200})$/i))if(id(a,"GASO",""+b),J._gasoDomain=a.get(Ra),J._gasoCPath=a.get(M),b=a=c[1],"adwords"!=b&&(b="www"),c="https://"+(b+".google.com")+"/analytics/reporting/overlay_js?gaso="+c[2]+(a?"&prefix="+a:"")+"&"+za())a=I.createElement("script"),a.type="text/javascript",a.async=h,a.src=c,a.id="_gasojs",ea(a,g),c=I.getElementsByTagName("script")[0],
c.parentNode.insertBefore(a,c);Fd=h}};var yd=function(a,b,c){c&&(b=G(b));c=a.b(L,1);b=b[w](".");!(2>b[u])&&/^\d+$/.test(b[0])&&(b[0]=""+c,id(a,"__utmx",b[A](".")))},zd=function(a,b){var c=Cc(a.get(L),V("__utmx"));"-"==c&&(c="");return b?Ba(c):c};var Ld=function(a,b){var c=k.min(a.b(lc,0),10);if(a.b(O,0)%100>=c)return j;c=Hd()||Id();if(c==g)return j;var d=c[0];if(d==g||d==ba||isNaN(d))return j;0<d?(1>1E3*k[sa]()&&F(124),Jd(c)?b(Kd(c)):b(Kd(c[ga](0,1)))):Ca(U,"load",function(){Ld(a,b)},j);return h},Jd=function(a){for(var b=1;b<a[u];b++)if(isNaN(a[b])||a[b]==ba||0>a[b])return j;return h},Kd=function(a){for(var b=new dd,c=0;c<a[u];c++)b.e(14,c+1,(isNaN(a[c])||0>a[c]?0:5E3>a[c]?10*k[ja](a[c]/10):45E4>a[c]?100*k[ja](a[c]/100):45E4)+""),b.k(14,
c+1,a[c]);return b},Hd=function(){var a=U.performance||U.webkitPerformance;if(a=a&&a.timing){var b=a.navigationStart;if(0==b)F(133);else return[a.loadEventStart-b,a.domainLookupEnd-a.domainLookupStart,a.connectEnd-a.connectStart,a.responseStart-a.requestStart,a.responseEnd-a.responseStart,a.fetchStart-b]}},Id=function(){if(U.top==U){var a=U.external,b=a&&a.onloadT;a&&!a.isValidLoadTime&&(b=g);2147483648<b&&(b=g);0<b&&a.setPageReadyTime();return b==g?g:[b]}};var R=function(a,b,c){function d(a){return function(b){if((b=b.get(rc)[a])&&b[u])for(var c=uc(e,a),d=0;d<b[u];d++)b[d].call(e,c)}}var e=this;this.a=new Bc;this.get=function(a){return this.a.get(a)};this.set=function(a,b,c){this.a.set(a,b,c)};this.set(Na,b||"UA-XXXXX-X");this.set(Pa,a||"");this.set(Oa,c||"");this.set(Qa,k.round((new Date).getTime()/1E3));this.set(M,"/");this.set(Sa,63072E6);this.set(Ua,15768E6);this.set(Ta,18E5);this.set(Va,j);this.set(mb,50);this.set(Wa,j);this.set(Xa,h);this.set(Ya,
h);this.set(Za,h);this.set($a,h);this.set(ab,h);this.set(cb,"utm_campaign");this.set(bb,"utm_id");this.set(db,"gclid");this.set(eb,"utm_source");this.set(fb,"utm_medium");this.set(gb,"utm_term");this.set(hb,"utm_content");this.set(ib,"utm_nooverride");this.set(jb,100);this.set(lc,1);this.set(pc,j);this.set(kb,"/__utm.gif");this.set(lb,1);this.set(qb,[]);this.set(N,[]);this.set(nb,od[ga](0));this.set(ob,[]);this.set(pb,[]);this.t("auto");this.set(vb,this.ra());this.set(rc,{hit:[],load:[]});this.a.g("0",
xd);this.a.g("1",md);this.a.g("2",ud);this.a.g("3",nd);this.a.g("4",d("load"));this.a.g("5",Gd);this.a.d("A",Pc);this.a.d("B",Rc);this.a.d("C",md);this.a.d("D",Oc);this.a.d("E",wc);this.a.d("F",Md);this.a.d("G",jd);this.a.d("H",Sc);this.a.d("I",Zc);this.a.d("J",gd);this.a.d("K",d("hit"));this.a.d("L",Nd);this.a.d("M",Od);0===this.get(Qa)&&F(111);this.a.J();this.w=g};D=R[v];D.h=function(){var a=this.get(rb);a||(a=new dd,this.set(rb,a));return a};
D.ta=function(a){for(var b in a){var c=a[b];a.hasOwnProperty(b)&&"function"!=typeof c&&this.set(b,c,h)}};D.z=function(a){if(this.get(pc))return j;var b=this,c=Ld(this.a,function(c){b.set(tb,a,h);b.u(c)});this.set(pc,c);return c};D.na=function(a){a&&a!=g&&-1<(a.constructor+"")[p]("String")?(F(13),this.set(tb,a,h)):"object"===typeof a&&a!==i&&this.ta(a);this.w=a=this.get(tb);1>=1E3*k[sa]()&&Pd();this.a.f("page");this.z(a)};
D.v=function(a,b,c,d,e){if(""==a||!ad(a)||""==b||!ad(b)||c!=g&&!ad(c)||d!=g&&!bd(d))return j;this.set(ec,a,h);this.set(fc,b,h);this.set(gc,c,h);this.set(hc,d,h);this.set(dc,!!e,h);this.a.f("event");return h};D.oa=function(a,b,c,d){if(!a||!b)return j;this.set(ic,a,h);this.set(jc,b,h);this.set(kc,c||I[x][ua],h);d&&this.set(tb,d,h);this.a.f("social");return h};D.ma=function(){this.set(lc,10);this.z(this.w)};D.pa=function(){this.a.f("trans")};D.u=function(a){this.set(sb,a,h);this.a.f("event")};
D.V=function(a){this.m();var b=this;return{_trackEvent:function(c,d,e){F(91);b.v(a,c,d,e)}}};D.Y=function(a){return this.get(a)};D.ga=function(a,b){if(a)if(a!=g&&-1<(a.constructor+"")[p]("String"))this.set(a,b);else if("object"==typeof a)for(var c in a)a.hasOwnProperty(c)&&this.set(c,a[c])};D.addEventListener=function(a,b){var c=this.get(rc)[a];c&&c[m](b)};D.removeEventListener=function(a,b){for(var c=this.get(rc)[a],d=0;c&&d<c[u];d++)if(c[d]==b){c.splice(d,1);break}};D.$=function(){return"5.2.3"};
D.t=function(a){this.get(Xa);a="auto"==a?Ga(I.domain):!a||"-"==a||"none"==a?"":a[C]();this.set(Ra,a)};D.ea=function(a){this.set(Xa,!!a)};D.Z=function(a,b){return Ad(this.a,a,b)};D.link=function(a,b){if(this.a.get(Va)&&a){var c=Ad(this.a,a,b);I[x].href=c}};D.da=function(a,b){this.a.get(Va)&&a&&a.action&&(a.action=Ad(this.a,a.action,b))};
D.ha=function(){this.m();var a=this.a,b=I.getElementById?I.getElementById("utmtrans"):I.utmform&&I.utmform.utmtrans?I.utmform.utmtrans:i;if(b&&b[la]){a.set(qb,[]);for(var b=b[la][w]("UTM:"),c=0;c<b[u];c++){b[c]=xa(b[c]);for(var d=b[c][w](Bd),e=0;e<d[u];e++)d[e]=xa(d[e]);"T"==d[0]?Dd(a,d[1],d[2],d[3],d[4],d[5],d[6],d[7],d[8]):"I"==d[0]&&Ed(a,d[1],d[2],d[3],d[4],d[5],d[6])}}};D.O=function(a,b,c,d,e,f,l,o){return Dd(this.a,a,b,c,d,e,f,l,o)};D.M=function(a,b,c,d,e,f){return Ed(this.a,a,b,c,d,e,f)};
D.ia=function(a){Bd=a||"|"};D.fa=function(a,b,c,d){var e=this.a;if(0>=a||a>e.get(mb)||!b||!c||128<b[u]+c[u])a=j;else{1!=d&&2!=d&&(d=3);var f={};fa(f,b);f.value=c;f.scope=d;e.get(N)[a]=f;a=h}a&&this.a.i();return a};D.X=function(a){this.a.get(N)[a]=g;this.a.i()};D.aa=function(a){return(a=this.a.get(N)[a])&&1==a[ra]?a[la]:g};D.ka=function(a,b,c){this.h().e(a,b,c)};D.la=function(a,b,c){this.h().k(a,b,c)};D.ba=function(a,b){return this.h().getKey(a,b)};D.ca=function(a,b){return this.h().C(a,b)};D.S=function(a){this.h().A(a)};
D.T=function(a){this.h().B(a)};D.W=function(){return new dd};D.K=function(a){a&&this.get(ob)[m](a[C]())};D.P=function(){this.set(ob,[])};D.L=function(a){a&&this.get(pb)[m](a[C]())};D.Q=function(){this.set(pb,[])};D.N=function(a,b,c,d,e){if(a&&b){a=[a,b[C]()][A](":");if(d||e)a=[a,d,e][A](":");d=this.get(nb);d.splice(c?0:d[u],0,a)}};D.R=function(){this.set(nb,[])};D.U=function(a){this.a[ia]();var b=this.get(M),c=zd(this.a);this.set(M,a);this.a.i();yd(this.a,c);this.set(M,b)};
D.ra=function(){var a="";try{var b=Ha(I[x][ua],j),a=da(Ea(b.c.get("utm_referrer")))||""}catch(c){F(146)}return a||I.referrer};D.m=function(){this.a[ia]()};D.ja=function(a){a&&""!=a&&(this.set(Eb,a),this.a.f("var"))};var Pd=function(){function a(a,b){(0==c[p](a)||-1<c[p]("; "+a))&&F(b)}function b(a,b){U[a]!==g&&F(b)}F(137);var c=I[ka];a("ga=",138);a("_ga=",139);a("ga2=",140);a("_a=",141);b("ga",142);b("_ga",143);b("ga2",144);b("_a",145)};var Md=function(a){"trans"!==a.get(ac)&&500<=a.b(Nb,0)&&a[qa]();if("event"===a.get(ac)){var b=(new Date).getTime(),c=a.b(Ob,0),d=a.b(Jb,0),c=k[ja](1*((b-(c!=d?c:1E3*c))/1E3));0<c&&(a.set(Ob,b),a.set(P,k.min(10,a.b(P,0)+c)));0>=a.b(P,0)&&a[qa]()}},Od=function(a){"event"===a.get(ac)&&a.set(P,k.max(0,a.b(P,10)-1))};var Qd=function(){var a=[];this.add=function(b,c,d){d&&(c=Ba(""+c));a[m](b+"="+c)};this.toString=function(){return a[A]("&")}},Rd=function(a,b){(b||2!=a.get(lb))&&a.n(Nb)},Sd=function(a,b){b.add("utmwv","5.2.3");b.add("utms",a.get(Nb));b.add("utmn",za());var c=I[x].hostname;E(c)||b.add("utmhn",c,h);c=a.get(jb);100!=c&&b.add("utmsp",c,h)},Ud=function(a,b){b.add("utmac",a.get(Na));a.get(dc)&&b.add("utmni",1);Td(a,b);J.q&&b.add("aip",1);b.add("utmu",Tc.Fa())},Td=function(a,b){function c(a,b){b&&d[m](a+
"="+b+";")}var d=[];c("__utma",Fc(a));c("__utmz",Lc(a,j));c("__utmv",Ic(a,h));c("__utmx",zd(a));b.add("utmcc",d[A]("+"),h)},Vd=function(a,b){a.get(Ya)&&(b.add("utmcs",a.get(Cb),h),b.add("utmsr",a.get(xb)),a.get(sc)&&b.add("utmvp",a.get(sc)),b.add("utmsc",a.get(yb)),b.add("utmul",a.get(Bb)),b.add("utmje",a.get(zb)),b.add("utmfl",a.get(Ab),h))},Wd=function(a,b){a.get(ab)&&a.get(ub)&&b.add("utmdt",a.get(ub),h);b.add("utmhid",a.get(wb));b.add("utmr",Ja(a.get(vb),a.get(M)),h);b.add("utmp",Ba(a.get(tb),
h),h)},Xd=function(a,b){for(var c=a.get(rb),d=a.get(sb),e=a.get(N)||[],f=0;f<e[u];f++){var l=e[f];l&&(c||(c=new dd),c.e(8,f,l[q]),c.e(9,f,l[la]),3!=l[ra]&&c.e(11,f,""+l[ra]))}!E(a.get(ec))&&!E(a.get(fc),h)&&(c||(c=new dd),c.e(5,1,a.get(ec)),c.e(5,2,a.get(fc)),e=a.get(gc),e!=g&&c.e(5,3,e),e=a.get(hc),e!=g&&c.k(5,1,e));c?b.add("utme",c.ua(d),h):d&&b.add("utme",d.o(),h)},Yd=function(a,b,c){var d=new Qd;Rd(a,c);Sd(a,d);d.add("utmt","tran");d.add("utmtid",b.id_,h);d.add("utmtst",b.affiliation_,h);d.add("utmtto",
b.total_,h);d.add("utmttx",b.tax_,h);d.add("utmtsp",b.shipping_,h);d.add("utmtci",b.city_,h);d.add("utmtrg",b.state_,h);d.add("utmtco",b.country_,h);!c&&Ud(a,d);return d[t]()},Zd=function(a,b,c){var d=new Qd;Rd(a,c);Sd(a,d);d.add("utmt","item");d.add("utmtid",b.transId_,h);d.add("utmipc",b.sku_,h);d.add("utmipn",b.name_,h);d.add("utmiva",b.category_,h);d.add("utmipr",b.price_,h);d.add("utmiqt",b.quantity_,h);!c&&Ud(a,d);return d[t]()},$d=function(a,b){var c=a.get(ac);if("page"==c)c=new Qd,Rd(a,b),
Sd(a,c),Xd(a,c),Vd(a,c),Wd(a,c),b||Ud(a,c),c=[c[t]()];else if("event"==c)c=new Qd,Rd(a,b),Sd(a,c),c.add("utmt","event"),Xd(a,c),Vd(a,c),Wd(a,c),!b&&Ud(a,c),c=[c[t]()];else if("var"==c)c=new Qd,Rd(a,b),Sd(a,c),c.add("utmt","var"),!b&&Ud(a,c),c=[c[t]()];else if("trans"==c)for(var c=[],d=a.get(qb),e=0;e<d[u];++e){c[m](Yd(a,d[e],b));for(var f=d[e].items_,l=0;l<f[u];++l)c[m](Zd(a,f[l],b))}else"social"==c?b?c=[]:(c=new Qd,Rd(a,b),Sd(a,c),c.add("utmt","social"),c.add("utmsn",a.get(ic),h),c.add("utmsa",a.get(jc),
h),c.add("utmsid",a.get(kc),h),Xd(a,c),Vd(a,c),Wd(a,c),Ud(a,c),c=[c[t]()]):c=[];return c},Nd=function(a){var b,c=a.get(lb),d=a.get(cc),e=d&&d.Aa,f=0;if(0==c||2==c){var l=a.get(kb)+"?";b=$d(a,h);for(var o=0,r=b[u];o<r;o++)La(b[o],e,l,h),f++}if(1==c||2==c){b=$d(a);o=0;for(r=b[u];o<r;o++)try{La(b[o],e),f++}catch(s){s&&Ka(s[q],g,s.message)}}d&&(d.j=f)};var ae="https:"==I[x].protocol?"https://ssl.google-analytics.com":"http://www.google-analytics.com",be=function(a){fa(this,"len");this.message=a+"-8192"},ce=function(a){fa(this,"ff2post");this.message=a+"-2036"},La=function(a,b,c,d){b=b||Aa;if(d||2036>=a[u])de(a,b,c);else if(8192>=a[u]){if(0<=U[va].userAgent[p]("Firefox")&&![].reduce)throw new ce(a[u]);ee(a,b)||fe(a,b)}else throw new be(a[u]);},de=function(a,b,c){var c=c||ae+"/__utm.gif?",d=new Image(1,1);d.src=c+a;ea(d,function(){ea(d,i);d.onerror=
i;b()});d.onerror=function(){ea(d,i);d.onerror=i;b()}},ee=function(a,b){var c,d=ae+"/p/__utm.gif",e=U.XDomainRequest;if(e)c=new e,c.open("POST",d);else if(e=U.XMLHttpRequest)e=new e,"withCredentials"in e&&(c=e,c.open("POST",d,h),c.setRequestHeader("Content-Type","text/plain"));if(c)return c.onreadystatechange=function(){4==c.readyState&&(b(),c=i)},c.send(a),h},fe=function(a,b){if(I.body){a=aa(a);try{var c=I.createElement('<iframe name="'+a+'"></iframe>')}catch(d){c=I.createElement("iframe"),fa(c,
a)}c.height="0";c.width="0";c.style.display="none";c.style.visibility="hidden";var e=I[x],e=ae+"/u/post_iframe.html#"+aa(e.protocol+"//"+e[na]+"/favicon.ico"),f=function(){c.src="";c.parentNode&&c.parentNode.removeChild(c)};Ca(U,"beforeunload",f);var l=j,o=0,r=function(){if(!l){try{if(9<o||c.contentWindow[x][na]==I[x][na]){l=h;f();Da(U,"beforeunload",f);b();return}}catch(a){}o++;ca(r,200)}};Ca(c,"load",r);I.body.appendChild(c);c.src=e}else Uc(function(){fe(a,b)},100)};var Y=function(){this.q=j;this.D={};this.F=[];this.wa=0;this._gasoCPath=this._gasoDomain=g;Q(Y[v],"_createTracker",Y[v].l,55);Q(Y[v],"_getTracker",Y[v].ya,0);Q(Y[v],"_getTrackerByName",Y[v].p,51);Q(Y[v],"_getTrackers",Y[v].za,130);Q(Y[v],"_anonymizeIp",Y[v].xa,16);tc()};D=Y[v];D.ya=function(a,b){return this.l(a,g,b)};D.l=function(a,b,c){b&&F(23);c&&F(67);b==g&&(b="~"+J.wa++);a=new R(b,a,c);J.D[b]=a;J.F[m](a);return a};D.p=function(a){a=a||"";return J.D[a]||J.l(g,a)};D.za=function(){return J.F[ga](0)};
D.xa=function(){this.q=h};var ge=function(a){if("prerender"==I.webkitVisibilityState)return j;a();return h};var J=new Y;var he=U._gat;he&&"function"==typeof he._getTracker?J=he:U._gat=J;var $c=new X;(function(a){if(!ge(a)){F(123);var b=j,c=function(){!b&&ge(a)&&(b=h,Da(I,"webkitvisibilitychange",c))};Ca(I,"webkitvisibilitychange",c)}})(function(){var a=U._gaq,b=j;if(a&&"function"==typeof a[m]&&(b="[object Array]"==Object[v][t].call(Object(a)),!b)){$c=a;return}U._gaq=$c;b&&$c[m].apply($c,a)});})();
