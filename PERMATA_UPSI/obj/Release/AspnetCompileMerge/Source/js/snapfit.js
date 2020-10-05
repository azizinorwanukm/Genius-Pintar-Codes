﻿/**
 * snapfit.js 1.5 (21-Jul-2011) (c) by Christian Effenberger 
 * All Rights Reserved. Source: snapfit.netzgesta.de
 * Distributed under Netzgestade Software License Agreement.
 * This license permits free of charge use on non-commercial 
 * and private web sites only under special conditions. 
 * Read more at... http://www.netzgesta.de/cvi/LICENSE.html
**/
eval(function (p, a, c, k, e, r) { e = function (c) { return (c < 62 ? '' : e(parseInt(c / 62))) + ((c = c % 62) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if ('0'.replace(0, e) == 0) { while (c--) r[e(c)] = k[c]; k = [function (e) { return r[e] || e }]; e = function () { return '([7ouA-QS-Z]|[1-7]\\w)' }; c = 1 }; while (c--) if (k[c]) p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]); return p }('V 2l,2W,B={version:1.5,released:\'2011-07-21 12:00:00\',4w:D,4x:D,4y:0,4z:0.33,4A:D,4B:D,4C:8,4D:0.5,3r:\'#ff0000\',3s:\'#e0e0e0\',4E:0.75,3t:\'#00d000\',3u:\'#000000\',4F:2,4G:3,4H:D,4I:D,4J:D,4K:D,4L:D,5V:P(1b,S){A(1b.2X.2Y()=="IMG"&&1b.Y>=4M&&1b.Z>=4M){P 5W(){V v=4N.parse(2D 4N())+L.5X(L.5Y()*100000000000);J v.3v(16)};P 2Z(3w){P 3x(v){J(L.1f(0,L.15(11(v,16),1E)))}J 3x(3w.4O(1,2))+\',\'+3x(3w.4O(3,2))+\',\'+3x(3w.4O(5,2))};P 5Z(v){J\'#\'+v.3v(16)+v.3v(16)+v.3v(16)};V 7,1i,31,32,3y=0,3z={"4P":B.4J,"4Q":B.4G,"4R":B.4y,"4S":B.4C,"4T":B.4A,"1X":B.4w,"4U":B.4I,"4V":B.4H,"3A":B.3u,"3B":B.3r,"3C":B.3t,"3D":B.3s,"4W":B.4z,"4X":B.4x,"4Y":B.4B,"4Z":B.4D,"50":B.4F,"51":B.4E,"52":B.4K,"53":B.4L};A(S){U(V i in 3z){A(!S[i]){S[i]=3z[i]}}}M{S=3z}31=(\'60\'in S)?11(S.60):1b.Y;32=(\'61\'in S)?11(S.61):1b.Z;A(W.all&&W.54&&!1u.62&&(!W.63||W.63<9)){A(W.54[\'v\']==1F){V e=["3F","shapetype","55","background","3G","formulas","handles","1R","3H","shadow","textbox","textpath","imagedata","line","polyline","curve","roundrect","oval","2m","arc","1b"],s=W.createStyleSheet();U(V i=0;i<e.Q;i++){s.addRule("v\\\\:"+e[i],"behavior: url(#default#VML);")}W.54.5V("v","urn:schemas-microsoft-com:1G")}V 34=(1b.65.2n.66()==\'2o\')?\'2o\':\'67-2o\';3y=1;7=W.2E([\'<V 1Y="on" O="2F:1;2n:\'+34+\';overflow:hidden;Y:\'+31+\'T;Z:\'+32+\'T;2G:1A;">\'].join(\'\'));V 57=1b.65.styleFloat.66();7.34=(57==\'1v\'||57==\'right\')?\'67\':34}M{7=W.2E(\'68\');1i=W.2E(\'68\')}A((3y&&7)||(1i&&7&&1i.22("2d")&&7.22("2d"))){7.id=(1b.id==\'69\'||1b.id==\'\'?5W():1b.id);7.1G=3y;7.1H=1b.1H;7.3I=1b.3I;7.1g=1b.3J;7.3K=1b.3K;7.O.2H=1b.O.2H;7.O.Z=32+\'T\';7.O.Y=31+\'T\';7.Z=32;7.Y=31;7.sf=L.15(1-L.15((1h S[\'4R\']===\'23\'?S[\'4R\']:B.4y)*0.01,0.5),1.0);7.iw=11(7.Y*7.sf);7.ih=11(7.Z*7.sf);7.H=11((7.Y-7.iw)/2);7.I=11((7.Z-7.ih)/2);7.2I=3L.appVersion.6a(\'WebKit\')!=-1&&!W.defaultCharset?1:0;7.3M=3L.6b.6a(\'Gecko\')>-1&&1u.updateCommands&&!1u.external?1:0;A(7.1G){1b.1x.59(7,1b)}M{1i.Z=7.ih;1i.Y=7.iw;1i.O.Z=1i.Z+\'T\';1i.O.Y=1i.Y+\'T\';A(7.2I){1i.id=7.id+\'6c\';1i.O.2p=\'fixed\';1i.O.1v=\'-99999px\';1i.O.1y=\'1A\';1b.1x.5a(1i)}1i.E=1i.22("2d");1i.E.2q(1b,0,0,7.iw,7.ih);1b.1x.59(7,1b);7.1i=1i}7.pc=L.15(L.1f((1h S[\'4Q\']===\'23\'?S[\'4Q\']:B.4G),0),6);7.sv=L.15(L.1f((1h S[\'4S\']===\'23\'?S[\'4S\']:B.4C),1),24);7.6d=(1h S[\'4V\']===\'26\'?S[\'4V\']:B.4H);7.6e=(1h S[\'52\']===\'26\'?S[\'52\']:B.4K);7.5b=(1h S[\'4U\']===\'26\'?S[\'4U\']:B.4I);7.1c=(1h S[\'4P\']===\'26\'?S[\'4P\']:B.4J);7.6f=(1h S[\'1X\']===\'26\'?S[\'1X\']:B.4w);7.3N=(1h S[\'4T\']===\'26\'?S[\'4T\']:B.4A);7.cb=(1h S[\'53\']===\'P\'?S[\'53\']:B.4L);7.bw=L.15(L.1f((1h S[\'50\']===\'23\'?S[\'50\']:B.4F),1.0),6.0);7.bo=L.15(L.1f((1h S[\'4Z\']===\'23\'?S[\'4Z\']:B.4D),0.0),1.0);7.ao=L.15(L.1f((1h S[\'4W\']===\'23\'?S[\'4W\']:B.4z),0.0),1.0);7.so=L.15(L.1f((1h S[\'51\']===\'23\'?S[\'51\']:B.4E),0.0),1.0);7.6g=5Z(11((1-7.so)*1E));7.18=300;7.19=D;7.ab=(1h S[\'4X\']===\'26\'?S[\'4X\']:B.4x);7.ai=(1h S[\'4Y\']===\'26\'?S[\'4Y\']:B.4B);7.1J=(1h S[\'3A\']===\'3O\'?S[\'3A\'].2L(/^#[0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f]$/i)?S[\'3A\']:B.3u:B.3u);7.3P=(1h S[\'3B\']===\'3O\'?S[\'3B\'].2L(/^#[0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f]$/i)?S[\'3B\']:B.3r:B.3r);7.5d=(1h S[\'3D\']===\'3O\'?S[\'3D\'].2L(/^#[0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f]$/i)?S[\'3D\']:B.3s:B.3s);7.3Q=(1h S[\'3C\']===\'3O\'?S[\'3C\'].2L(/^#[0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f][0-9a-f]$/i)?S[\'3C\']:B.3t:B.3t);7.6h=3L.6b.2L(/android/i);7.6i=3L.platform.2L(/iPad|iPhone|iPod/i);7.6j=1h(W.6k)!="69"?X:D;7.6l=7.6j&&(7.6i||7.6h||7.6e)?X:D;7.14=L.15(7.iw,7.ih)/(7.1c?2:1);7.sz=[L.15(7.14,160),L.15(7.14,4M),L.15(7.14,104),L.15(7.14,80),L.15(7.14,64),L.15(7.14,56),L.15(7.14,48)];7.2M=2Z(7.1J);7.5e=2Z(7.3P);7.5f=2Z(7.5d);7.6n=2Z(7.3Q);7.3R=0;7.3S=0;7.O.MozUserSelect="3T";7.O.KhtmlUserSelect="3T";7.O.WebkitUserSelect="3T";7.O.WebkitTouchCallout="3T";7.1Y="on";7.36=B.6o(7);7.37=B.6p(7);7.38=0;7.39=0;7.1K=0;7.1L=0;7.1j=D;7.10=D;7.1T=D;7.2N=D;7.C=-1;B.5g(7,7.pc,X);A(7.3N){B.3U(7,7.6f,X)}A(7.1G){7.2a=P(){J D};A(!7.5b){7.onmouseenter=u.6q;7.5h=u.6r}}M{7.E=7.22("2d");7.E.shadowOffsetX=4;7.E.shadowOffsetY=4;7.E.shadowBlur=6;7.E.5i="1k(0,0,0,0)";A(7.6l){7.6k=u.6s}M{7.5j=u.6t;7.5k=u.6u;7.5l=u.5m;7.5n=u.6v;7.onmouseout=u.5m;7.onclick=P(){J D};7.2a=P(){J D};A(!7.5b){7.onmouseover=u.6w;7.3V(\'mouseout\',u.6x,D)}}}B.1z(7);J X}J D}J D},remove:P(7){A(!7.10&&(7.22||7.2X.2Y()=="3W")){V o=7.1x,1g=W.2E(\'1g\');A(7.2I){o.6y(W.2b(7.id+\'6c\'))}1g.id=7.id;1g.1H=7.1H;1g.3I=7.3I;1g.3J=7.1g;1g.3K=7.3K;1g.O.2H=7.O.2H;1g.O.Z=7.Z+\'T\';1g.O.Y=7.Y+\'T\';o.59(1g,7)}},5g:P(7,5o,2c){A(!7.10&&(7.22||7.2X.2Y()=="3W")){7.10=X;7.O.1s=7.1G?\'5p\':\'10\';7.pc=L.15(L.1f((1h 5o===\'23\'?5o:7.pc),0),6);V i,j,a,t,m,H,I,wp,F,hp,G,tw,th,lt,lc,lb,ct,cc,cb,rt,rc,rb,z=0,n=7.sz[7.pc],p={};t=7.iw/n;wp=L.6z(t);tw=7.iw/wp;F=tw/7.iw;H=F*.25;t=7.ih/n;hp=L.6z(t);th=7.ih/hp;G=th/7.ih;I=G*.25;m=[[0,0],[0,G],[F,G],[F,0]];7.o=[];A(7.1c){p.lt=[[0,0],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0]];p.6A=B.1d(p.lt,7.18,7.19);p.ct=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0]];p.6B=B.1d(p.ct,7.18,7.19);p.rt=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,0]];p.6C=B.1d(p.rt,7.18,7.19);p.lc=[[0,0],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0],[F-H,0],[F/2,I],[H,0]];p.6D=B.1d(p.lc,7.18,7.19);p.cc=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0],[F-H,0],[F/2,I],[H,0]];p.6E=B.1d(p.cc,7.18,7.19);p.rc=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[H,G],[F/2,G+I],[F-H,G],[F,G],[F,0],[F-H,0],[F/2,I],[H,0]];p.6F=B.1d(p.rc,7.18,7.19);p.lb=[[0,0],[0,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0],[F-H,0],[F/2,I],[H,0]];p.6G=B.1d(p.lb,7.18,7.19);p.cb=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[F,G],[F,G-I],[F+H,G/2],[F,I],[F,0],[F-H,0],[F/2,I],[H,0]];p.6H=B.1d(p.cb,7.18,7.19);p.rb=[[0,0],[0,I],[H,G/2],[0,G-I],[0,G],[F,G],[F,0],[F-H,0],[F/2,I],[H,0]];p.6I=B.1d(p.rb,7.18,7.19);U(i=0;i<hp;i++){A(i==0){U(j=0;j<wp;j++){7.o[z]=2D 47();7.o[z].a=0;7.o[z].f=0;7.o[z].m=0;7.o[z].x=(j*F);7.o[z].y=(i*G);A(j==0){7.o[z].s=0;7.o[z].w=F+H;7.o[z].h=G+I;7.o[z].t=p.lt;7.o[z].l=p.6A}M A(j==(wp-1)){7.o[z].s=1;7.o[z].w=F;7.o[z].h=G+I;7.o[z].t=p.rt;7.o[z].l=p.6C}M{7.o[z].s=0;7.o[z].w=F+H;7.o[z].h=G+I;7.o[z].t=p.ct;7.o[z].l=p.6B}z++}}M A(i==(hp-1)){U(j=0;j<wp;j++){7.o[z]=2D 47();7.o[z].a=0;7.o[z].f=0;7.o[z].m=0;7.o[z].x=(j*F);7.o[z].y=(i*G);A(j==0){7.o[z].s=2;7.o[z].w=F+H;7.o[z].h=G;7.o[z].t=p.lb;7.o[z].l=p.6G}M A(j==(wp-1)){7.o[z].s=3;7.o[z].w=F;7.o[z].h=G;7.o[z].t=p.rb;7.o[z].l=p.6I}M{7.o[z].s=2;7.o[z].w=F+H;7.o[z].h=G;7.o[z].t=p.cb;7.o[z].l=p.6H}z++}}M{U(j=0;j<wp;j++){7.o[z]=2D 47();7.o[z].a=0;7.o[z].f=0;7.o[z].m=0;7.o[z].x=(j*F);7.o[z].y=(i*G);A(j==0){7.o[z].s=0;7.o[z].w=F+H;7.o[z].h=G+I;7.o[z].t=p.lc;7.o[z].l=p.6D}M A(j==(wp-1)){7.o[z].s=1;7.o[z].w=F;7.o[z].h=G+I;7.o[z].t=p.rc;7.o[z].l=p.6F}M{7.o[z].s=0;7.o[z].w=F+H;7.o[z].h=G+I;7.o[z].t=p.cc;7.o[z].l=p.6E}z++}}}}M{U(i=0;i<hp;i++){U(j=0;j<wp;j++){7.o[z]=2D 47();7.o[z].a=0;7.o[z].f=0;7.o[z].m=0;7.o[z].x=(j*F);7.o[z].y=(i*G);7.o[z].w=F;7.o[z].h=G;7.o[z].t=m;z++}}}U(i=0;i<7.o.Q;i++){a=[];t=7.o[i].t;U(j=0;j<t.Q;j++){a[j]=[t[j][0]*7.iw,t[j][1]*7.ih]}7.o[i].q=X;7.o[i].z=X;7.o[i].t=a;7.o[i].w=7.o[i].w*7.iw;7.o[i].h=7.o[i].h*7.ih;7.o[i].x=7.H+(7.o[i].x*7.iw);7.o[i].y=7.I+(7.o[i].y*7.ih);7.o[i].ox=7.o[i].x-7.H;7.o[i].oy=7.o[i].y-7.I;7.o[i].ow=7.o[i].w;7.o[i].oh=7.o[i].h}A(7.1G){V N,5q,5r,1R,5s,5t=\'\';5q=\'<v:55 2a="J D" 1Y="on" O="2F:1;2n:\'+7.34+\';3a:1A;2G:1A;2p:relative;Y:\'+7.Y+\'T;Z:\'+7.Z+\'T;" 49="\'+7.Y+\',\'+7.Z+\'"><v:2m 2a="J D" 1Y="on" 2s="0" 2O="f" 2P="f" 2s="0" O="2F:1;3a:0;2G:0;2n:2o;2p:4a;1y:1A;1v:1A;Y:\'+7.Y+\'T;Z:\'+7.Z+\'T;"></v:2m>\';5r=\'</v:55>\';5s=\'<v:2m 2a="J D" 1Y="on" 2O="t" 2P="\'+(!7.ai&&7.ab?\'t\':\'f\')+\'" 2s="\'+(!7.ai&&7.ab?1:0)+\'" O="2F:1;3a:1A;2G:1A;2n:2o;2p:4a;1y:\'+7.I+\'T;1v:\'+7.H+\'T;Y:\'+7.iw+\'T;Z:\'+7.ih+\'T; 1U:6J(2t=\'+(7.ai?0:7.ao*2g)+\')"><v:1R 2u="\'+7.5d+\'" /></v:2m>\';1R=\'<v:2m 2a="J D" 1Y="on" 2O="t" 2P="\'+(7.ai&&7.ab?\'t\':\'f\')+\'" 2s="\'+(7.ai&&7.ab?1:0)+\'" O="2F:1;3a:1A;2G:1A;2n:2o;2p:4a;1y:\'+7.I+\'T;1v:\'+7.H+\'T;Y:\'+7.iw+\'T;Z:\'+7.ih+\'T; 1U:6J(2t=\'+(7.ai?7.ao*2g:0)+\')"><v:1R 3J="\'+7.1g+\'" 6K="6L" 6M="6N" /></v:2m>\';V p,l,wl,hl,k=0,x=.5,y=.5,w=4d(1/wp),h=4d(1/hp);tw=wp;th=hp;A(7.1c){wl=(7.o[7.o.Q-1].w/7.o[0].w);hl=(7.o[7.o.Q-1].h/7.o[0].h);A(wp<=2){H=0.5-((1-wl)/1.5)}M{H=0.5-(0.5*(1-wl)/(wp*.5))}A(hp<=2){I=0.5-((1-hl)/1.5)}M{I=0.5-(0.5*(1-hl)/(hp*.5))}}U(i=0;i<hp;i++){U(j=0;j<wp;j++){t=7.o[k].t;A(7.1c){tw=4d(7.iw/7.o[k].w);th=4d(7.ih/7.o[k].h);A(7.o[k].s==3){x=.5;y=.5}M{x=7.o[k].s==0||7.o[k].s==2?H:.5;y=7.o[k].s==0||7.o[k].s==1?I:.5}}p="m "+11(t[0][0]*2g)+","+11(t[0][1]*2g);U(l=1;l<t.Q;l++){p+=" l "+11(t[l][0]*2g)+","+11(t[l][1]*2g)}p+=" x e";5t+=\'<v:3F id="\'+7.id+\'|2Q\'+k+\'" 2a="J D" 1Y="on" 2O="t" 2P="t" 2s="1" 2R="\'+7.1J+\'" 5u="0,0" 49="\'+11(7.o[k].w*2g)+\',\'+11(7.o[k].h*2g)+\'" 3G="\'+p+\'" O="1s:6O;2F:1;3a:1A;2G:1A;2n:2o;2p:4a;1y:\'+7.o[k].y+\'T;1v:\'+7.o[k].x+\'T;Y:\'+7.o[k].w+\'T;Z:\'+7.o[k].h+\'T; 1U:2h:2i.2j.2v(2w=0, 2x=0);"><v:1R 2u="\'+7.1J+\'" 2t="1" 3J="\'+7.1g+\'" 6K="6L" 6M="6N" size="\'+tw+\',\'+th+\'" origin="\'+((j+x-(wp*x))*w)+\',\'+((i+y-(hp*y))*h)+\'" 2p="0,0" /></v:3F>\';k++}}7.innerHTML=5q+5s+1R+5t+5r;U(i=0;i<7.o.Q;i++){N=W.2b(7.id+\'|2Q\'+i);N.5n=B.5v;N.5j=B.5w}}A(1u.62){U(i=0;i<7.o.Q;i++){7.o[i].ow=7.o[i].ow-0.6P;7.o[i].oh=7.o[i].oh-0.6P}}A(!2c){B.1z(7)}7.O.1s=\'2y\';7.10=D;7.1T=X;J X}M{J D}},3U:P(7,1X,2c){1X=1X||D;A(!7.10&&(7.22||7.2X.2Y()=="3W")){P R(v){J L.5X(L.5Y()*v)}V N,i,j,l,r,a=90;7.1T=D;7.2N=D;7.10=X;A(7.1G){7.O.1s=\'5p\';U(i=0;i<7.o.Q;i++){N=W.2b(7.id+\'|2Q\'+i);A(!1X){l=R(4);7.o[i].a=l*90;7.o[i].f=R(2);N.O.1U="2h:2i.2j.2v(2w="+11(7.o[i].a/90)+", 2x="+7.o[i].f+")";N.1M.2u=7.1J;N.1M.2t=7.o[i].f?7.bo:1}7.o[i].x=R(7.Y-7.o[i].w);7.o[i].y=R(7.Z-7.o[i].h);N.O.1v=7.o[i].x+\'T\';N.O.1y=7.o[i].y+\'T\'}}M{7.O.1s=\'10\';U(i=0;i<7.o.Q;i++){7.o[i].x=R(7.Y-7.o[i].w);7.o[i].y=R(7.Z-7.o[i].h);l=R(4);A(!1X){U(j=0;j<l;j++){r=7.o[i].a;7.o[i]=B.3b(7.o[i],7.Y,7.Z,a,7.1c,7.18,7.19);7.o[i].a=r<(2S-a)?7.o[i].a+a:0}A(R(2)){7.o[i].t=B.2z(7.o[i].t,7.o[i].x,7.o[i].w);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(7.o[i].a==90||7.o[i].a==1N){7.o[i].m=7.o[i].m?0:1}M{7.o[i].f=7.o[i].f?0:1}}A(R(2)){7.o[i].t=B.2A(7.o[i].t,7.o[i].y,7.o[i].h);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(7.o[i].a==90||7.o[i].a==1N){7.o[i].f=7.o[i].f?0:1}M{7.o[i].m=7.o[i].m?0:1}}}}A(!2c){B.1z(7)}}U(i=0;i<7.o.Q;i++){p=7.o[i];A((p.f&&p.m&&p.a==1V)||(!p.f&&!p.m&&p.a==0)){A(p.x!=(p.ox+7.H)||p.x!=(p.ox+7.H)||p.y!=(p.oy+7.I)||p.y!=(p.oy+7.I)){7.o[i].z=X}M{7.o[i].z=D}}M{7.o[i].z=D}}U(i=0;i<7.o.Q;i++){7.o[i].q=D}7.O.1s=\'2y\';7.10=D;7.1T=D;7.2N=X;J X}M{J D}},6Q:P(7,2c){A(!7.10&&(7.22||7.2X.2Y()=="3W")){V i,j,l,p,r,1n=0,m=4,a=90,n=B.5x(7);A(!n){A(7.1G){U(i=0;i<7.o.Q;i++){1n=W.2b(7.id+\'|2Q\'+i);7.o[i].f=0;7.o[i].a=0;7.o[i].w=7.o[i].ow;7.o[i].h=7.o[i].oh;1n.1M.2t=1;1n.O.1U="2h:2i.2j.2v(2w=0, 2x=0)"}A(!2c){A(7.2k){1u.4e(7.2k)}7.10=X;7.O.1s=\'5p\';V q=0,c=0,t=20,k=1/t,sx=[],sy=[],ex=[],ey=[];U(i=0;i<7.o.Q;i++){sx[i]=7.o[i].x;sy[i]=7.o[i].y;ex[i]=7.o[i].ox+7.H;ey[i]=7.o[i].oy+7.I}7.2k=1u.6R(P(){q=((-L.4f((k*c)*L.PI)/2)+0.5)||0;U(i=0;i<7.o.Q;i++){1n=W.2b(7.id+\'|2Q\'+i);1n.O.1v=L.4g(sx[i]+(q*(ex[i]-sx[i])))+\'T\';1n.O.1y=L.4g(sy[i]+(q*(ey[i]-sy[i])))+\'T\'}c++;A(c>t){1u.4e(7.2k);U(i=0;i<7.o.Q;i++){7.o[i].x=7.o[i].ox+7.H;7.o[i].y=7.o[i].oy+7.I;7.o[i].z=X;1n=W.2b(7.id+\'|2Q\'+i);1n.O.1v=7.o[i].x+\'T\';1n.O.1y=7.o[i].y+\'T\'}7.O.1s=\'2y\';7.10=D}},30)}}M{U(i=0;i<7.o.Q;i++){1n=7.o[i].a==90||7.o[i].a==1N?1:0;A((7.o[i].m&&1n)||7.o[i].f){7.o[i].t=B.2z(7.o[i].t,7.o[i].x,7.o[i].w);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(1n){7.o[i].m=7.o[i].m?0:1}M{7.o[i].f=7.o[i].f?0:1}}A((7.o[i].f&&1n)||7.o[i].m){7.o[i].t=B.2A(7.o[i].t,7.o[i].y,7.o[i].h);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(1n){7.o[i].f=7.o[i].f?0:1}M{7.o[i].m=7.o[i].m?0:1}}A((7.o[i].m&&1n)||7.o[i].f){7.o[i].t=B.2z(7.o[i].t,7.o[i].x,7.o[i].w);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(1n){7.o[i].m=7.o[i].m?0:1}M{7.o[i].f=7.o[i].f?0:1}}A((7.o[i].f&&1n)||7.o[i].m){7.o[i].t=B.2A(7.o[i].t,7.o[i].y,7.o[i].h);A(7.1c){7.o[i].l=B.1d(7.o[i].t,7.18,7.19)}A(1n){7.o[i].f=7.o[i].f?0:1}M{7.o[i].m=7.o[i].m?0:1}}n=7.o[i].a/90;l=n>0?m-n:0;U(j=0;j<l;j++){r=7.o[i].a;7.o[i]=B.3b(7.o[i],7.Y,7.Z,a,7.1c,7.18,7.19);7.o[i].a=r<(2S-a)?7.o[i].a+a:0}A(2c){7.o[i].x=7.o[i].ox+7.H;7.o[i].y=7.o[i].oy+7.I;7.o[i].z=X}}A(!2c){A(7.2k){1u.4e(7.2k)}7.10=X;7.O.1s=\'10\';V q=0,c=0,t=20,k=1/t,sx=[],sy=[],ex=[],ey=[];U(i=0;i<7.o.Q;i++){sx[i]=7.o[i].x;sy[i]=7.o[i].y;ex[i]=7.o[i].ox+7.H;ey[i]=7.o[i].oy+7.I}7.2k=1u.6R(P(){q=((-L.4f((k*c)*L.PI)/2)+0.5)||0;U(i=0;i<7.o.Q;i++){7.o[i].x=L.4g(sx[i]+(q*(ex[i]-sx[i])));7.o[i].y=L.4g(sy[i]+(q*(ey[i]-sy[i])))}c++;B.1z(7);A(c>t){1u.4e(7.2k);U(i=0;i<7.o.Q;i++){7.o[i].x=7.o[i].ox+7.H;7.o[i].y=7.o[i].oy+7.I;7.o[i].z=X}B.1z(7);7.O.1s=\'2y\';7.10=D}},30)}}U(i=0;i<7.o.Q;i++){7.o[i].q=X}7.1T=X;J X}M{J D}}M{J D}},1z:P(7){A(!7.1G&&7.o){V p,c,k,i,j,4h,4i,5y,1g=7.1i;7.E.clearRect(0,0,7.Y,7.Z);7.E.2T="1k("+7.5f+","+7.ao+")";A(7.ab){7.E.6S=1;7.E.1W="1k(0,0,0,"+7.ao+")";7.E.3c(7.H,7.I,7.iw,7.ih)}A(7.ai){7.E.6T=7.ao;7.E.2q(1g,7.H,7.I,7.iw,7.ih);7.E.6T=1.0}M{7.E.3d(7.H,7.I,7.iw,7.ih)}7.E.5z();U(i=0;i<7.o.Q;i++){p=7.o[i];7.E.5z();A(i==7.C){7.E.5i="1k(0,0,0,"+7.so+")"}7.E.2T="1k("+7.5f+",1)"||"1k(1E,1E,1E,1)";7.E.3e();7.E.3f(p.x+p.t[0][0],p.y+p.t[0][1]);U(j=1;j<p.t.Q;j++){7.E.3g(p.x+p.t[j][0],p.y+p.t[j][1])}7.E.4j();7.E.1R();A(i==7.C){7.E.5i="1k(0,0,0,0)"}A(7.2I){7.E.3e();7.E.3f(p.x+p.t[0][0],p.y+p.t[0][1]);U(j=1;j<p.t.Q;j++){7.E.3g(p.x+p.t[j][0],p.y+p.t[j][1])}7.E.4j()}7.E.clip();A(7.3M){7.E.2T="1k(0,0,0,0)";7.E.3d(p.x,p.y,p.w,p.h)}A(p.a!=0||p.f||p.m){7.E.5z();4h=p.x;4i=p.y;5y=(p.a+(p.a>=0?0:2S))*L.PI/1V;7.E.6U(4h,4i);7.E.rotate(5y);7.E.scale((!p.f?1:-1),(!p.m?1:-1));7.E.6U(-4h,-4i);A(p.a==90){7.E.2q(1g,p.ox,p.oy,p.ow,p.oh,p.x-(p.f*p.ow),p.y-p.oh+(p.m*p.oh),p.ow,p.oh)}M A(p.a==1V){7.E.2q(1g,p.ox,p.oy,p.ow,p.oh,p.x-p.ow+(p.f*p.ow),p.y-p.oh+(p.m*p.oh),p.ow,p.oh)}M A(p.a==1N){7.E.2q(1g,p.ox,p.oy,p.ow,p.oh,p.x-p.ow+(p.f*p.ow),p.y-(p.m*p.oh),p.ow,p.oh)}M{7.E.2q(1g,p.ox,p.oy,p.ow,p.oh,p.x-(p.f*p.ow),p.y-(p.m*p.oh),p.ow,p.oh)}7.E.5A()}M{7.E.2q(1g,p.ox,p.oy,p.ow,p.oh,p.x,p.y,p.w,p.h)}7.E.6S=7.bw*2;A(!7.10&&i==7.C){A((!p.f&&p.m)||(p.f&&!p.m)){7.E.2T="1k("+7.2M+","+7.bo+")";7.E.3d(p.x,p.y,p.w,p.h)}A((p.f&&p.m&&p.a==1V)||(!p.f&&!p.m&&p.a==0)){A(p.x<=(p.ox+7.H+7.sv)&&p.x>=(p.ox+7.H-7.sv)&&p.y<=(p.oy+7.I+7.sv)&&p.y>=(p.oy+7.I-7.sv)){7.E.1W="1k("+7.6n+",1)"}M{7.E.1W="1k("+7.5e+",1)"}}M{7.E.1W="1k("+7.5e+",1)"}A(7.1c){A(7.2I||7.3M){7.E.3e();7.E.3f(p.x+p.t[0][0],p.y+p.t[0][1]);U(j=1;j<p.t.Q;j++){7.E.3g(p.x+p.t[j][0],p.y+p.t[j][1])}7.E.4j()}7.E.3H()}M{7.E.3c(p.x,p.y,p.w,p.h)}}M{A(7.1c){A((!p.f&&p.m)||(p.f&&!p.m)){7.E.2T="1k("+7.2M+","+7.bo+")";7.E.3d(p.x,p.y,p.w,p.h);7.E.1W="1k("+7.2M+",1)";A(7.2I||7.3M){7.E.3e();7.E.3f(p.x+p.t[0][0],p.y+p.t[0][1]);U(j=1;j<p.t.Q;j++){7.E.3g(p.x+p.t[j][0],p.y+p.t[j][1])}7.E.4j()}7.E.3H()}M{U(j=0;j<p.t.Q;j++){c=p.l[j];k=j<p.t.Q-1?j+1:0;7.E.1W="1k("+c+","+c+","+c+","+7.bo+")";7.E.3e();7.E.3f(p.x+p.t[j][0],p.y+p.t[j][1]);7.E.3g(p.x+p.t[k][0],p.y+p.t[k][1]);7.E.3H()}}}M{A((!p.f&&p.m)||(p.f&&!p.m)){7.E.2T="1k("+7.2M+","+7.bo+")";7.E.3d(p.x,p.y,p.w,p.h);7.E.1W="1k("+7.2M+",1)";7.E.3c(p.x,p.y,p.w,p.h)}M{7.E.1W="1k(0,0,0,"+7.bo+")";7.E.3c(p.x-7.bw,p.y-7.bw,p.w+7.bw,p.h+7.bw);7.E.1W="1k(1E,1E,1E,"+7.bo+")";7.E.3c(p.x,p.y,p.w+7.bw,p.h+7.bw)}}}7.E.5A()}7.E.5A()}J D},6s:P(e){A(u.o&&e.2U.Q==1&&!u.10){e.preventDefault();u.C=-1;P 4k(a,i){V t=a[i];a.3h(i,1);a.5B(t);J a};u.1j=D;u.3R=2D 4N().getTime();u.1K=e.2U[0].4l-u.36;u.1L=e.2U[0].4m-u.37;V 5C=(u.3R-u.3S)||0;A(5C<400&&5C>0){u.3S=-1;u.C=B.3i(u);A(u.C>-1){V r=90,p=u.o[u.C];A(p.f&&!p.m){u.o[u.C].t=B.2z(p.t,p.x,p.w);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].m=p.m?0:1}M{u.o[u.C].f=p.f?0:1}}M A(!p.f&&p.m){u.o[u.C].t=B.2A(p.t,p.y,p.h);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].f=p.f?0:1}M{u.o[u.C].m=p.m?0:1}}M{u.o[u.C]=B.3b(p,u.Y,u.Z,r,u.1c,u.18,u.19);u.o[u.C].a=p.a<(2S-r)?p.a+r:0}B.1w(u,u.C);u.C=-1;B.1z(u);B.2C(u,u.C)}}M{u.3S=u.3R;u.C=B.3i(u);A(u.C>-1){A(u.C!=u.o.Q-1){u.o=4k(u.o,u.C);u.C=u.o.Q-1}u.38=u.1K-u.o[u.C].x;u.39=u.1L-u.o[u.C].y;u.6V=B.6W;u.6X=B.6Y;u.1j=X}}}J D},6W:P(e){A(e.2U.Q==1&&u.1j&&!u.10){u.1K=e.2U[0].4l-u.36;u.1L=e.2U[0].4m-u.37;u.o[u.C].x=L.15(u.Y-u.o[u.C].w,L.1f(0,u.1K-u.38));u.o[u.C].y=L.15(u.Z-u.o[u.C].h,L.1f(0,u.1L-u.39));B.1z(u)}J D},6Y:P(){A(u.10){J D}A(u.1j&&!u.10){u.6V=1F;u.6X=1F;u.1j=D;B.1w(u,u.C);u.C=-1;B.1z(u);B.2C(u,u.C)}J D},6t:P(e){A(u.10){J D}P 4k(a,i){V t=a[i];a.3h(i,1);a.5B(t);J a};A(u.o){u.1K=e.4l-u.36;u.1L=e.4m-u.37;u.C=B.3i(u);A(u.C>-1){V p=u.o[u.C];A(u.C!=u.o.Q-1){u.o=4k(u.o,u.C);u.C=u.o.Q-1}A(e.5D==2){u.o[u.C].t=B.2z(p.t,p.x,p.w);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].m=p.m?0:1}M{u.o[u.C].f=p.f?0:1}B.1w(u,u.C);u.C=-1}M A(e.5D==3){u.o[u.C].t=B.2A(p.t,p.y,p.h);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].f=p.f?0:1}M{u.o[u.C].m=p.m?0:1}B.1w(u,u.C);u.C=-1}M{u.1j=X;u.O.1s=\'6Z\';u.38=u.1K-u.o[u.C].x;u.39=u.1L-u.o[u.C].y}B.1z(u)}}J D},6u:P(e){A(!u.1j||u.10){J D}u.1K=e.4l-u.36;u.1L=e.4m-u.37;u.o[u.C].x=L.15(u.Y-u.o[u.C].w,L.1f(0,u.1K-u.38));u.o[u.C].y=L.15(u.Z-u.o[u.C].h,L.1f(0,u.1L-u.39));B.1z(u);J D},5m:P(e){A(!u.1j||u.10){J D}u.1j=D;B.1w(u,u.C);u.O.1s=\'2y\';u.C=-1;B.1z(u);B.2C(u,u.C);J D},6v:P(e){V 2V=e.70,1H=e.71;u.1j=D;u.O.1s=\'2y\';u.C=-1;A(u.o){u.C=B.3i(u);A(u.C>-1){V r=90,p=u.o[u.C];A(!2V&&!1H){u.o[u.C]=B.3b(p,u.Y,u.Z,r,u.1c,u.18,u.19);u.o[u.C].a=p.a<(2S-r)?p.a+r:0;B.1w(u,u.C);u.C=-1}M A(2V&&!1H){u.o[u.C].t=B.2z(p.t,p.x,p.w);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].m=p.m?0:1}M{u.o[u.C].f=p.f?0:1}B.1w(u,u.C);u.C=-1}M A(!2V&&1H){u.o[u.C].t=B.2A(p.t,p.y,p.h);A(u.1c){u.o[u.C].l=B.1d(u.o[u.C].t,u.18,u.19)}A(p.a==90||p.a==1N){u.o[u.C].f=p.f?0:1}M{u.o[u.C].m=p.m?0:1}B.1w(u,u.C);u.C=-1}B.1z(u);B.2C(u,u.C)}}J D},5w:P(e){e=e?e:1u.4n;V N,4o,f,r,d,14=e.5E,c=14.id.4p("_"),i=11(c[c.Q-1]),o=14.1x,K=14.1x.1x;A(!K.10){A(e.button==2){K.1j=D;K.o[i].f=K.o[i].f?0:1;14.1M.2u=K.1J;14.1M.2t=K.o[i].f?K.bo:1;14.O.1U="2h:2i.2j.2v(2w="+11(K.o[i].a/90)+", 2x="+K.o[i].f+")";B.1w(K,i);14.O.1v=K.o[i].x+\'T\';14.O.1y=K.o[i].y+\'T\'}M{r=11(K.o[i].a);A(r==0||r==1V){d=r+135}M{d=r-45}d=K.o[i].f?d+90:d;14.O.1U="2h:2i.2j.Shadow(Color="+K.6g+", direction="+d+"), 2h:2i.2j.2v(2w="+11(K.o[i].a/90)+", 2x="+K.o[i].f+")";N=W.2E(\'v:3F\');N.id=14.id;N.1Y="on";N.2a=P(){J D};N.2O=14.2O;N.2P=14.2P;N.2s=14.2s;N.2R=14.2R;N.5u=14.5u;N.49=14.49;N.3G=14.3G;N.O.2H=14.O.2H;4o=W.2E(\'v:1R\');4o=14.1M;o.6y(14);o.5a(N);N.5a(4o);N.O.1s="6Z";N.1M.2u=K.1J;N.5F=e.5G;N.5H=e.5I;N.5n=B.5v;N.5j=B.5w;N.5h=B.3j;N.2R=B.1w(K,i)?K.3Q:K.3P;W.3k("5k",B.5J);W.3k("5l",B.3j);2W=N.id;K.1j=X}}J D},5J:P(e){e=e?e:1u.4n;V l,t,N=e.5E,c=N.id.4p("_"),i=11(c[c.Q-1]),K=N.1x.1x;A(!K.10&&K.1j){A(2W==N.id){l=L.1f(0,L.15(K.Y-11(K.o[i].w),11(N.O.1v)+(e.5G-N.5F)));t=L.1f(0,L.15(K.Z-11(K.o[i].h),11(N.O.1y)+(e.5I-N.5H)));K.o[i].x=l;K.o[i].y=t;N.1M.2u=K.1J;N.O.1v=l+\'T\';N.O.1y=t+\'T\';N.5F=e.5G;N.5H=e.5I;N.2R=B.1w(K,i)?K.3Q:K.3P}M{B.3j(e)}}J D},3j:P(e){e=e?e:1u.4n;V f,N=W.2b(2W),c=N.id.4p("_"),i=11(c[c.Q-1]),K=N.1x.1x;A(N&&!K.10&&K.1j){2W=1F;N.O.1U="2h:2i.2j.2v(2w="+11(K.o[i].a/90)+", 2x="+K.o[i].f+")";B.1w(K,i);N.O.1s="6O";N.5h=1F;N.2R=K.1J;N.O.1v=K.o[i].x+\'T\';N.O.1y=K.o[i].y+\'T\';W.3l("5k",B.5J);W.3l("5l",B.3j);K.1j=D;B.2C(K,K.C)}J D},5v:P(e){e=e?e:1u.4n;V f,N=e.5E,c=N.id.4p("_"),i=11(c[c.Q-1]),K=N.1x.1x,2V=e.70,1H=e.71,r=90;A(!K.10){K.1j=D;N.O.1s=\'2y\';A(!2V&&!1H){K.o[i].a=K.o[i].a<(2S-r)?K.o[i].a+r:0;c=K.o[i].w;K.o[i].w=K.o[i].h;K.o[i].h=c}M{K.o[i].f=K.o[i].f?0:1;N.1M.2u=K.1J;N.1M.2t=K.o[i].f?K.bo:1}N.O.1U="2h:2i.2j.2v(2w="+11(K.o[i].a/90)+", 2x="+K.o[i].f+")";B.1w(K,i);N.O.1v=K.o[i].x+\'T\';N.O.1y=K.o[i].y+\'T\';B.2C(K,K.C)}J D},3m:P(e){A(2l!=1F){V k,72=46,73=27,74=13,76=8,7=W.2b(2l);A(7.1j||7.10){J D}k=(e.77?e.77:e.5D);switch(k){4q 73:B.5g(7);4r;4q 74:B.6Q(7);4r;4q 76:B.3U(7,X);4r;4q 72:B.3U(7,D);4r}}J D},3n:P(e){J D},3o:P(e){J D},6w:P(e){2l=u.id;u.78();W.3V(\'79\',B.3o,D);W.3V(\'7a\',B.3n,D);W.3V(\'7b\',B.3m,D);J D},6x:P(e){2l=1F;W.5K(\'79\',B.3o,D);W.5K(\'7a\',B.3n,D);W.5K(\'7b\',B.3m,D);J D},6q:P(e){2l=u.id;u.78();W.3k("7c",B.3o);W.3k("7d",B.3n);W.3k("7e",B.3m);J D},6r:P(e){2l=1F;W.3l("7c",B.3o);W.3l("7d",B.3n);W.3l("7e",B.3m);J D},2C:P(7){A(!7.1j||!7.10){A(!7.1T&&7.2N&&B.5x(7)&&7.cb){7.1T=X;7.2N=D;7.cb()}M{7.1T=D}}J D},5x:P(7){A(7.2N&&!7.1T){U(V i=0;i<7.o.Q;i++){A(!7.o[i].q){J D}}J X}M{J D}},7f:P(7,C){P 5L(a,t){V d=a[t];a.3h(t,1);a.7g(d);J a};7.o=5L(7.o,C);7.o.C=0;J D},7h:P(7,C){P 7i(a,t){V d=a[t];a.3h(t,1);a.5B(d);J a};P 5L(a,t){V d=a[t];a.3h(t,1);a.7g(d);J a};V w,h,c,p=7.o[C],3N=p.x,7j=p.y,1f=p.x+p.w,7k=p.y+p.h;U(i=7.o.Q-1;i>-1;i--){A(i!=C){c=7.o[i];w=c.x+c.w;h=c.y+c.h;A((w>=3N&&c.x<=1f)&&(h>=7j&&c.y<=7k)){A(i!=7.o.Q-1){7.o=7i(7.o,i);7.o.C=7.o.Q-1}}}}J D},3i:P(7){V i,j,k,c,p,l,t,x=7.1K,y=7.1L;U(i=7.o.Q-1;i>-1;i--){j=0;k=0;c=0;p=7.o[i].t;l=7.o[i].x;t=7.o[i].y;U(k in p){j++;A(j==p.Q){j=0}A((((t+p[k][1])<y)&&((t+p[j][1])>=y))||(((t+p[j][1])<y)&&((t+p[k][1])>=y))){A((l+p[k][0])+(y-(t+p[k][1]))/((t+p[j][1])-(t+p[k][1]))*((l+p[j][0])-(l+p[k][0]))<x){c=!c}}}A(c){J i}}J-1},1w:P(7,C){V p=7.o[C];A((p.f&&p.m&&p.a==1V)||(!p.f&&!p.m&&p.a==0)){A(p.x<=(p.ox+7.H+7.sv)&&p.x>=(p.ox+7.H-7.sv)&&p.y<=(p.oy+7.I+7.sv)&&p.y>=(p.oy+7.I-7.sv)){7.o[C].x=p.ox+7.H;7.o[C].y=p.oy+7.I;7.o[C].z=X;7.o[C].q=X;A(!7.1G){A(7.6d){B.7h(7,C)}M{B.7f(7,C)}}J X}}7.o[C].z=D;7.o[C].q=D;J D},3b:P(p,w,h,r,z,d,k){P 7l(v,z){J v[0]-z[0]};P 7m(v,z){J v[1]-z[1]};V j,x,y,T,py,mx=0,my=0,mw=1,mh=1,a=[],b=[],H=p.x+(.5*p.w),I=p.y+(.5*p.h),g=r*(L.PI/1V),cs=L.4f(g),sn=L.sin(g);U(j=0;j<p.t.Q;j++){T=p.x+p.t[j][0];py=p.y+p.t[j][1];x=(T-H)*cs-(py-I)*sn+H;y=(py-I)*cs+(T-H)*sn+I;a[j]=[x,y]}b=a.slice();b.7n(7l);mx=b[0][0];mw=b[b.Q-1][0];b.7n(7m);my=b[0][1];mh=b[b.Q-1][1];p.w=mw-mx;p.h=mh-my;p.x=mx;p.y=my;U(j=0;j<a.Q;j++){a[j][0]=a[j][0]-p.x;a[j][1]=a[j][1]-p.y}p.x=L.1f(L.15(w-p.w,p.x),0);p.y=L.1f(L.15(h-p.h,p.y),0);p.t=a;A(z){p.l=B.1d(a,d,k)}J p},2z:P(t,tx,tw){V j,x,T,H=tx+(.5*tw),a=[];U(j=0;j<t.Q;j++){T=tx+t[j][0];A(T<H){x=H+(H-T);a[j]=[L.1f(x-tx,0),t[j][1]]}M A(T>H){x=H-(T-H);a[j]=[L.1f(x-tx,0),t[j][1]]}M{a[j]=[t[j][0],t[j][1]]}}J a},2A:P(t,ty,th){V j,y,py,I=ty+(.5*th),a=[];U(j=0;j<t.Q;j++){py=ty+t[j][1];A(py<I){y=I+(I-py);a[j]=[t[j][0],L.1f(y-ty,0)]}M A(py>I){y=I-(py-I);a[j]=[t[j][0],L.1f(y-ty,0)]}M{a[j]=[t[j][0],t[j][1]]}}J a},1d:P(t,d,k){k=k||0;V i,ag,ad,at,av,dx,sx,ex,dy,sy,ey,ls=(d-1V)*(L.PI/1V),z=[];P 7p(n){A(n>=0){J n}M{J 0-n}};P 7q(x,y){V a=L.atan2(y,x);A(a<0.0){a=2*L.PI+a}J a};U(i=0;i<t.Q;i++){sx=t[i][0];sy=t[i][1];A(i<t.Q-1){ex=t[i+1][0];ey=t[i+1][1]}M{ex=t[0][0];ey=t[0][1]}dy=(ey-sy);dx=(sx-ex);ag=2*L.PI-7q(dx,dy);ad=7p(ls-ag);at=0.5*(L.4f(ad)+1.0);av=11(at*0+(1.0-at)*1E);z[i]=!k?av>127?1E:0:av}J z},6o:P(n){V r=n.7r;U(V p=n;p=p.7s;p!=1F){r+=p.7r}J r},6p:P(n){V r=n.7t;U(V p=n;p=p.7s;p!=1F){r+=p.7t}J r},_swap:P(a,i){t=a[i];a[i]=a[a.Q-1];a[a.Q-1]=t;J a}}', [], 464, '|||||||self|||||||||||||||||obj||||||this||||||if|snapfit|cur|false|ctx|wf|hf|xo|yo|return|par|Math|else|ele|style|function|length||options|px|for|var|document|true|width|height|progress|parseInt|||tmp|min|||lsd|nhc||image|ply|_shade||max|img|typeof|reso|dragging|rgba|||odd|||||cursor||window|left|_isCatched|parentNode|top|_paint|0px||||255|null|vml|alt||bcl|evtX|evtY|firstChild|270||||fill||solved|filter|180|strokeStyle|simple|unselectable||||getContext|number|||boolean||||oncontextmenu|getElementById|option||||100|progid|DXImageTransform|Microsoft|timer|cvi_stactive|rect|display|block|position|drawImage||strokeweight|opacity|color|BasicImage|rotation|mirror|auto|_flipY|_flipX||_ifSolved|new|createElement|zoom|padding|cssText|wk4|||match|dbc|admixed|filled|stroked|obj_|strokecolor|360|fillStyle|touches|shift|cvi_stpiece|tagName|toUpperCase|hex2rgb||imageWidth|imageHeight||dpl||offsetX|offsetY|deltaX|deltaY|margin|_rotate|strokeRect|fillRect|beginPath|moveTo|lineTo|splice|_isContained|_ieReleased|attachEvent|detachEvent|_isKeydown|_isKeypressed|_isKeyup|||defaultFalsecolor|defaultAreacolor|defaultMatchcolor|defaultBgrndcolor|toString|val|h2d|isIE|defopts|bcolor|fcolor|mcolor|acolor||shape|path|stroke|title|src|className|navigator|ge8|mix|string|fcl|mcl|currtap|lasttap|none|admix|addEventListener|VAR|||||||||||Object||coordsize|absolute|||parseFloat|clearInterval|cos|ceil|xoff|yoff|closePath|last|pageX|pageY|event|fil|split|case|break|||||defaultSimple|defaultAreaborder|defaultSpace|defaultAreaopacity|defaultMixed|defaultAreaimage|defaultSnap|defaultBorderopacity|defaultShadowopacity|defaultBorderwide|defaultLevel|defaultTofront|defaultNokeys|defaultPolygon|defaultForcetouchui|defaultCallback|128|Date|substr|polygon|level|space|snap|mixed|nokeys|tofront|aopacity|aborder|aimage|bopacity|bwide|sopacity|forcetui|callback|namespaces|group||flt||replaceChild|appendChild|nak||acl|dfc|dac|reset|onmouseleave|shadowColor|onmousedown|onmousemove|onmouseup|_isReleased|ondblclick|pres|wait|head|foot|back|parts|coordorigin|_ieDblclicked|_iePressed|_isSolved|angle|save|restore|push|delay|which|srcElement|mouseX|clientX|mouseY|clientY|_ieDragged|removeEventListener|setFirst||||||||||add|uniqueID|floor|random|dec2hex|iwidth|iheight|opera|documentMode||currentStyle|toLowerCase|inline|canvas|undefined|indexOf|userAgent|_buffer|tof|fui|pos|hxs|aos|ios|stg|ontouchstart|tod||dmc|_xoff|_yoff|_catchEnter|_catchLeave|_catchTouch|_isPressed|_isDragged|_isDblclicked|_catchOver|_catchOut|removeChild|round|ltz|ctz|rtz|lcz|ccz|rcz|lbz|cbz|rbz|Alpha|type|frame|aspect|atleast|hand|001|solve|setInterval|lineWidth|globalAlpha|translate|ontouchmove|_touchMove|ontouchend|_touchEnd|move|shiftKey|altKey|del|esc|ent||bsp|keyCode|focus|keyup|keypress|keydown|onkeyup|onkeypress|onkeydown|_setBehind|unshift|_isBehind|setLast|miy|may|sdx|sdy|sort||fabs|theta|offsetLeft|offsetParent|offsetTop'.split('|'), 0, {}))