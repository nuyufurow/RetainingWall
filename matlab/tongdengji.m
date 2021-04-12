function [A] = tongdengji( C )
%UNTITLED3 次子函数将不同标号的混凝土的材料信息集成
%   仅支持C20~C50的混凝土强度等级，单位均为MPa
format bank;    %%%%%%%%显示小数点后两位，计算精度不会变
if C==20,
    fc=9.6;Ec=2.55*10e4;ftk=1.54;ft=1.10;
elseif C==25,
    fc=11.9;Ec=2.80*10e4;ftk=1.78;ft=1.27;
elseif C==30,
    fc=14.3;Ec=3.00*10e4;ftk=2.01;ft=1.43;
elseif C==35,
    fc=16.7;Ec=3.15*10e4;ftk=2.20;ft=1.57;  
elseif C==40,
    fc=19.1;Ec=3.25*10e4;ftk=2.39;ft=1.71;
elseif C==45,
    fc=21.1;Ec=3.35*10e4;ftk=2.51;ft=1.80;
elseif C==50,
    fc=23.1;Ec=3.45*10e4;ftk=2.64;ft=1.89;
else 
    disp('请输入正确的混凝土强度等级!!!');
    return
end
    A=[fc Ec ftk ft];
end

