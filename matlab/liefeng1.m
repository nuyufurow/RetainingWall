function [ w ] = liefeng1(M,cs,d,As,h,C,rg )
%���Ӻ������ڼ����ѷ��ȣ���liefeng�Ӻ�����ͬ�����Ӻ��������ڵ�������ѷ����
alphacr=1.9;
CC=tongdengji(C);                 %�����Ӻ�������ȡ����������ǿ�����ֵ
ftk=CC(3);     
Mq=M/rg;                                                                   %����׼���ú���������ע��ͬliefeng�Ӻ���

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%�����ѷ���   
                                                                           %d�����и�ʽ��Ϊ[ֱ��1  ����1  ֱ��2  ����2]������ʱ��ֱ��2������2ֵΪ0
    deq=(d(2)*d(1)^2+d(4)*d(3)^2)/(d(2)*d(1)+d(4)*d(3));                   %��Чֱ������
    d=max(d(1),d(3));                                                      %Ϊ��ѡ���ָֽ�ֱ���Ľϴ�ֵ 
    h0=h-cs-d/2;                                                           %Ϊ����߶�
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

