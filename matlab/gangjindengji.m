function [ A ] = gangjindengji( F )
%UNTITLED3 ���Ӻ�������ͬ��ŵĻ������Ĳ�����Ϣ����
%��֧��HPB300��HRB335��HRB400��HRB500�ĸֽ����ǿ�ȵ�λ��ΪMPa
format bank;    %%%%%%%%��ʾС�������λ�����㾫�Ȳ����
if F==300,
    fy=270;
elseif F==335,
    fy=300;
elseif F==400,
    fy=360;
elseif F==500,
    fy=435;
else 
    disp('��������ȷ�ĸֽ�ȼ�!!!');
end
    A=[fy];
end