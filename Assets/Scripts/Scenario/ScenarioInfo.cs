namespace Vampire.Scenario
{
    public struct ScenarioInfo
    {
        public readonly string type;
        public readonly string option;
        public readonly string message;
        public readonly string rinaFace;
        public readonly string adolfFace;
        public readonly string rinaActive;
        public readonly string adolfActive;

        public ScenarioInfo(string[] line)
        {
            
            this.type = line[0];
            this.option = line[1];
            this.message = line[2];
            this.rinaFace = line[3] ;
            this.adolfFace = line[4];
            this.rinaActive = line[5];
            this.adolfActive = line[6];
        }
    }
}
