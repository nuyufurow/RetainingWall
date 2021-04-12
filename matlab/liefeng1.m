function [ w ] = liefeng1(M,cs,d,As,h,C,rg )
%本子函数用于计算裂缝宽度，与liefeng子函数不同，此子函数仅用于单个点的裂缝计算
alphacr=1.9;
CC=tongdengji(C);                 %调用子函数，提取混凝土抗拉强度设计值
ftk=CC(3);     
Mq=M/rg;                                                                   %计算准永久荷载内力，注解同liefeng子函数

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%计算裂缝宽度   
                                                                           %d的所有格式均为[直径1  数量1  直径2  数量2]，跨中时，直径2和数量2值为0
    deq=(d(2)*d(1)^2+d(4)*d(3)^2)/(d(2)*d(1)+d(4)*d(3));                   %等效直径计算
    d=max(d(1),d(3));                                                      %为所选两种钢筋直径的较大值 
    h0=h-cs-d/2;                                                           %为计算高度
    seigemas=abs(Mq)/(0.87*h0*As);
    Ate=0.5*1000*h;
    route=As/Ate;
    if route<0.01
       route=0.01;
    end
    fai=1.1-0.65*ftk/(route*seigemas);
    if fai<0.2
        fai=0.2;
    elseif fai>1.0
        fai=1.0;
    elseif fai>=0.2 && fai<=1.0
        fai=fai;
    end
    w=alphacr*fai*seigemas*(1.9*cs+0.08*deq/route)/200000;
   
end

