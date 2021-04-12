function [ A ] = gangjindengji( F )
%UNTITLED3 次子函数将不同标号的混凝土的材料信息集成
%仅支持HPB300、HRB335、HRB400、HRB500的钢筋，屈服强度单位均为MPa
format bank;    %%%%%%%%显示小数点后两位，计算精度不会变
if F==300,
    fy=270;
elseif F==335,
    fy=300;
elseif F==400,
    fy=360;
elseif F==500,
    fy=435;
else 
    disp('请输入正确的钢筋等级!!!');
end
    A=[fy];
end