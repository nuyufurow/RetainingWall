function [A] = tongdengji( C )
%UNTITLED3 ���Ӻ�������ͬ��ŵĻ������Ĳ�����Ϣ����
%   ��֧��C20~C50�Ļ�����ǿ�ȵȼ�����λ��ΪMPa
format bank;    %%%%%%%%��ʾС�������λ�����㾫�Ȳ����
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
    disp('��������ȷ�Ļ�����ǿ�ȵȼ�!!!');
    return
end
    A=[fc Ec ftk ft];
end

