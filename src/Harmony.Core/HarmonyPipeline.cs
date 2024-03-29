namespace Harmony.Core;

public class HarmonyPipeline
{
    public async Task SendAsync(Command harmonyCommand)
    {
        
    }
    
    public async Task SendAsync<TOutput>(Command<TOutput> harmonyCommand)
    {
        
    }
    
    public async Task SendAsync<TInput, TOutput>(Command<TInput, TOutput> harmonyCommand)
    {
        
    }
    
    public async Task SendAsync(Query harmonyQuery)
    {
        
    }
    
    public async Task SendAsync<TOutput>(Query<TOutput> harmonyQuery)
    {
        
    }
    
    public async Task SendAsync<TInput, TOutput>(Query<TInput, TOutput> harmonyQuery)
    {
        
    }
}