function [ Ass ] = zuhezhengfujin( n,As,ft,fy,h,M,cs,C,rg )
%���Ӻ������ڽ�����ѡ��͸���ѡ������ϳ�һ�����󣬷����������
%Asz����Ϊ����ѡ�����
%Asf150����Ϊ����ѡ�������150
%Asf200����Ϊ����ѡ�������200
%Ass����Ϊ�����������
%As����ΪΪpeijinjisuan( M,cs,n,fy,fc,ft)�м���õ�����ļ������
if n==1
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];
    
elseif n==2
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg);
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];   
   
elseif n==3
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];   
   
elseif n==4
        Asz150=zhengjinxuanjin150(n,As,h,M,cs,C,rg); 
        Asz200=zhengjinxuanjin200(n,As,h,M,cs,C,rg);
        Asf150=fujinxuanjin150(n,As,ft,fy,h,M,cs,C,rg);    
        Asf200=fujinxuanjin200(n,As,ft,fy,h,M,cs,C,rg); 
        Ass=[Asf150;
             Asf200;
             Asz150;
             Asz200];
end
end